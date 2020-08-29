using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities
{
    public interface IValidity
    {
        bool IsValid { get; set; }
    }
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
    public interface IValidateableObject<T> : IValidity
    {
        List<string> Errors { get; }
        T Value { get; }
        List<IValidationRule<T>> ValidationsRules { get; }

        bool Validate();

        void ValidationsClear();

        ICommand ValidateCommand { get; }

        void Set(T value);
    }
    public class ValidateableObject<T> : ExtendedBindableObject, IValidateableObject<T> where T : new()
    {
        private List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid = false;
        private ICommand _validateCommmand;

        public List<IValidationRule<T>> ValidationsRules
        {
            get { return _validations; }
            protected set { _validations = value; }
        }

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ICommand ValidateCommand
        {
            get { return _validateCommmand; }
            set { _validateCommmand = value; }
        }

        public ValidateableObject()
        {
            _value = new T();
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            _validateCommmand = new Command(() => Validate());
        }

        public ValidateableObject(T value)
        {
            _value = value;
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            _validateCommmand = new Command(() => Validate());
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public void ValidationsClear()
        {
            ValidationsRules = new List<IValidationRule<T>>();
        }

        public void Set(T value)
        {
            Value = value;
        }
    }
}
