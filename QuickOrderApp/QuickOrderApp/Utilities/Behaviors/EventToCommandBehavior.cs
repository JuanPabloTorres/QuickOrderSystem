using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Behaviors
{
    public class CommandEventData
    {
        public dynamic? Sender { get; set; }
        public dynamic? Parameter { get; set; }
        public dynamic? Args { get; set; }
    }
    public class EventToCommandBehavior : BindableBehavior<View>
    {

        public static BindableProperty EventNameProperty =
            BindableProperty.CreateAttached("EventName", typeof(string), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty EventArgsConverterProperty =
            BindableProperty.CreateAttached("EventArgsConverter", typeof(IValueConverter), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        public static BindableProperty EventArgsConverterParameterProperty =
            BindableProperty.CreateAttached("EventArgsConverterParameter", typeof(object), typeof(EventToCommandBehavior), null,
                BindingMode.OneWay);

        protected Delegate? _handler;
        private EventInfo? _eventInfo;

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter EventArgsConverter
        {
            get { return (IValueConverter)GetValue(EventArgsConverterProperty); }
            set { SetValue(EventArgsConverterProperty, value); }
        }

        public object EventArgsConverterParameter
        {
            get { return GetValue(EventArgsConverterParameterProperty); }
            set { SetValue(EventArgsConverterParameterProperty, value); }
        }

        protected override void OnAttachedTo(Xamarin.Forms.View visualElement)
        {
            base.OnAttachedTo(visualElement);

            var events = AssociatedObject?.GetType().GetRuntimeEvents().ToArray();
            if (events.Any())
            {
                _eventInfo = events.FirstOrDefault(e => e.Name == EventName);
                if (_eventInfo == null)
                    throw new ArgumentException(String.Format("EventToCommand: Can't find any event named '{0}' on attached type", EventName));

                MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnFired");
                _handler = methodInfo.CreateDelegate(_eventInfo.EventHandlerType, this);
                _eventInfo.AddEventHandler(AssociatedObject, _handler);
                //AddEventHandler(_eventInfo, AssociatedObject, OnFired);
            }
        }

        protected override void OnDetachingFrom(Xamarin.Forms.View view)
        {
            if (_handler != null)
                _eventInfo?.RemoveEventHandler(AssociatedObject, _handler);

            base.OnDetachingFrom(view);
        }

        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            _handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
            )
            .Compile();

            eventInfo.AddEventHandler(item, _handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            CommandEventData commandEventData;
            object eventArgsparameter;

            if (Command == null)
                return;

            eventArgsparameter = eventArgs;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                if (EventArgsConverter != null)
                {
                    eventArgsparameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, System.Globalization.CultureInfo.CurrentUICulture);
                }
            }

            commandEventData = new CommandEventData
            {
                Parameter = CommandParameter,
                Args = eventArgsparameter,
                Sender = sender
            };

            if (Command.CanExecute(commandEventData))
            {
                Command.Execute(commandEventData);
            }
        }
    }
}
