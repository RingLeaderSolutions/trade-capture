using System;
using Automatonymous;
using EDF.TradeCapture.Messaging.Events;
using EDF.TradeCapture.Persistence.Model;
using EDF.TradeCapture.StateSaga.Commands;
using MassTransit;
using Serilog;

namespace EDF.TradeCapture.StateSaga.StateMachine
{
    public class TradeStateMachine : MassTransitStateMachine<PersistedTrade>
    {
        public State ValidatingData { get; private set; }

        public State SendingTradeToPump { get; private set; }

        public State Completed { get; private set; }


        public Event<INewTradeReceived> NewTradeReceived { get; private set; }

        public Event<IValidationCompletedEvent> ValidationCompleted { get; private set; }

        public Event<ITradeSentToPomaxPumpEvent> TradeSentToPomaxPump { get; private set; }

        public Event<ITradeSentToGeSwapPumpEvent> TradeSentToGeSwapPump { get; private set; }


        public TradeStateMachine()
        {
            InstanceState(s => s.CurrentState);


            Event(() => NewTradeReceived,
                x =>
                {
                    x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId);
                    x.InsertOnInitial = true;
                    x.SetSagaFactory(context => new PersistedTrade
                    {
                        CorrelationId = NewId.NextGuid(),
                        TradeId = context.Message.TradeId
                    });
                    x.SelectId(context => NewId.NextGuid());
                });

            Event(() => ValidationCompleted,
                x => x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId));

            Event(() => TradeSentToPomaxPump,
                x => x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId));

            Event(() => TradeSentToGeSwapPump,
                x => x.CorrelateBy(sagaState => sagaState.TradeId, context => context.Message.TradeId));


            Initially(
                When(NewTradeReceived)
                    .Then(
                        context =>
                            Log.Information("[TradeStateMachine] New Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .Send((state, command) => new Uri(StateSagaConfiguration.ValidationDataEndpoint),
                        context => new ValidationRequestedCommand(context.Data.TradeId))
                    .TransitionTo(ValidatingData));


            During(ValidatingData,
                When(ValidationCompleted)
                    .Then(
                        context =>
                            Log.Information(
                                "[TradeStateMachine] Trade validated: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .If(context => context.Data.TradeId.Contains("0"), 
                        binder => binder.Send((state, command) => new Uri(StateSagaConfiguration.PomaxPumpEndpoint),
                        context => new SendTradeToPomaxPumpCommand(context.Data.TradeId)))
                    .If(context => !context.Data.TradeId.Contains("0"), 
                        binder => binder.Send((state, command) => new Uri(StateSagaConfiguration.GeSwapPumpEndpoint),
                        context => new SendTradeToGeSwapPumpCommand(context.Data.TradeId)))
                    .TransitionTo(SendingTradeToPump));


            During(SendingTradeToPump,
                When(TradeSentToPomaxPump)
                    .Then(
                        context =>
                            Log.Information(
                                "[TradeStateMachine] Trade sent to Pomax Pump: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .TransitionTo(Completed));
            

            During(SendingTradeToPump,
                When(TradeSentToGeSwapPump)
                    .Then(
                        context =>
                            Log.Information(
                                "[TradeStateMachine] Trade sent to GE Swap Pump: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .TransitionTo(Completed));


            During(Completed,
                When(NewTradeReceived)
                    .Then(
                        context =>
                            Log.Information("[TradeStateMachine] Existing Trade: Id=[{CorrelationId}] TradeId=[{TradeId}]",
                                context.Instance.CorrelationId,
                                context.Data.TradeId))
                    .Send((state, command) => new Uri(StateSagaConfiguration.ValidationDataEndpoint),
                        context => new ValidationRequestedCommand(context.Data.TradeId))
                    .TransitionTo(ValidatingData));
        }
    }
}