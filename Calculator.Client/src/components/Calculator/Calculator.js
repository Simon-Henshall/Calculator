import React, { Component } from 'react';
import './Calculator.css';

export class Calculator extends Component {
  static displayName = Calculator.name;

  render () {
    // React doesn't support calc() in external .css files so we need to inline them
    // https://github.com/kristerkari/react-native-css-modules/issues/9
    const buttonCalc = {
      flex: '1 0 calc((100% / 3) - 10px)'
    };
    const smallCalc = {
      flex: '1 0 calc((25% / 3) - 10px)'
    };

    return (
      <div id="calculator">
        <div id="output" className="button" style={buttonCalc}></div>
        <div className="break"></div>
        <div id="divide" className="button small operator" style={smallCalc}>/</div>
        <div id="times" className="button small operator" style={smallCalc}>X</div>
        <div id="minus" className="button small operator" style={smallCalc}>-</div>
        <div id="plus" className="button small operator" style={smallCalc}>+</div>
        <div className="break"></div>
        <div id="num7" className="button operand" style={buttonCalc}>7</div>
        <div id="num8" className="button operand" style={buttonCalc}>8</div>
        <div id="num9" className="button operand" style={buttonCalc}>9</div>
        <div id="num4" className="button operand" style={buttonCalc}>4</div>
        <div id="num5" className="button operand" style={buttonCalc}>5</div>
        <div id="num6" className="button operand" style={buttonCalc}>6</div>
        <div id="num1" className="button operand" style={buttonCalc}>1</div>
        <div id="num2" className="button operand" style={buttonCalc}>2</div>
        <div id="num3" className="button operand" style={buttonCalc}>3</div>
        <div id="clear" className="button operator" style={buttonCalc}>C</div>
        <div id="num0" className="button operand" style={buttonCalc}>0</div>
        <div id="equals" className="button operator" style={buttonCalc}>=</div>
      </div>
    );
  }
}

/*

// Set defaults
let result = '';
let validSymbols = ['+', '-', 'X', '/'];
let lastClicked = '';

const buttons = document.getElementsByClassName('button');
for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', function () {
        // ToDo: Need a global clear-at-any-stage handler
        // ToDo: Multiple digits -- only single digits supported at the moment

        // Equals functionality
        if (this.innerHTML === "=") {
            // First we work out what the input parses to
            const parsedInput = result.split(/([0-9]+)([\+\-X\/]+)([0-9]+)/).filter(a => a);
            // Then we craft the payload
            const request = JSON.stringify({
                Operand1: parseInt(parsedInput[0]),
                Symbol: parsedInput[1],
                Operand2: parseInt(parsedInput[2])
            });
            // The we send the data for processing server-side
            $.ajax({
                url: '/Calculator/Calculate',
                type: 'POST',
                dataType: "json",
                contentType: 'application/json',
                data: request,
                // Then we process the response
                success: function (data) {
                    result = data.result;
                    document.getElementById('output').innerHTML = result;
                    result = ''; // Clear the input after a calculation
                }
            });
            lastClicked = '=';
        }

        // Clear functionality
        else if (this.innerHTML === "C") {
            result = '';
            lastClicked = 'C';
        }

        // Main functionality
        else {
            // Check for length constraints and exclude the equals from showing in the result
            if (result.length < 10 && this.innerHTML !== '=') {
                result += this.innerHTML;
                lastClicked = 'generic';
            }
        }

        // Don't clear the result for multiple equal presses
        if (lastClicked !== '=') {
            document.getElementById('output').innerHTML = result;
        }
    });
}

*/