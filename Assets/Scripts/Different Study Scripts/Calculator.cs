using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    public class Calculator : MonoBehaviour
    {
        public string[] OperationsNames;
        
        [SerializeField] private InputField _firstNumberInputField;
        [SerializeField] private InputField _secondNumberInputField;
        [SerializeField] private Dropdown _operationMath;
        [SerializeField] private Button _calculateButton;
        [SerializeField] private Text _resultText;

        
        private void Awake()
        {
            
            _operationMath.options.Clear();
            foreach (var operationsName in OperationsNames)
            {
                _operationMath.options.Add(new Dropdown.OptionData(operationsName));
            }
            CalculatorLogic calculatorLogic = new CalculatorLogic();
            calculatorLogic.OnCalcResult += SetResult;
            _operationMath.onValueChanged.AddListener(
                (value) =>
                {
                    var opName = _operationMath.options[value].text;
                    calculatorLogic.SetOperationValue(opName);
                }
                );
            
            _secondNumberInputField.onValueChanged.AddListener(calculatorLogic.SetSecondNumber);
            _firstNumberInputField.onValueChanged.AddListener(calculatorLogic.SetFirstNumber);
            _calculateButton.onClick.AddListener(() =>
            {
                calculatorLogic.Calculate();
            });
        }
        
        public void SetResult(float number)
        {
            _resultText.text = number.ToString();
        }
        
    }

   
   // public delegate float MyDelegate(float firstNumber, float secondNumber);
    public class CalculatorLogic
    {
        public Func<float, float, float> CalculationContainer;
        //public Calculator calculator;
        public float FirstNumber;
        public float SecondNumber;

        public event Action<float> OnCalcResult; 


        public void SetOperationValue(string value)
        {
            switch (value)
            {
                case "+":
                    CalculationContainer = SumMethod;
                    break;
                case "-":
                    CalculationContainer = SubstractionMethod; 
                    break;
                case "*":
                    CalculationContainer = MultiplyMethod;
                    break;
                case "/":
                    CalculationContainer = DivisionMethod;
                    break;
            }
            Debug.Log($"dropdown: {value}");
        }

        public void SetFirstNumber(string number)
        {
            FirstNumber = float.Parse(number);
            Debug.Log("первое число: " + FirstNumber);
        }

        public void SetSecondNumber(string number)
        {
            SecondNumber = float.Parse(number);
            Debug.Log("второе число: " + SecondNumber);
        }
        
        public void Calculate()
        {
            var result = CalculationContainer?.Invoke(FirstNumber, SecondNumber);
            var finalResult = result ?? 0f;
            OnCalcResult?.Invoke(finalResult);
        }

        private float SumMethod(float a, float b)
        {
            return a + b;
        }
        
        private float SubstractionMethod(float a, float b)
        {
            return a - b;
        }
        private float MultiplyMethod(float a, float b)
        {
            return a * b;
        }
        private float DivisionMethod(float a, float b)
        {
            return a / b;
        }
    }
}
