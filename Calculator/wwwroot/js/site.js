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