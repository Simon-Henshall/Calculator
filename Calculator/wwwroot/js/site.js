// Set defaults
let clickCount = 0;
let click1 = 0;
let click2 = '+';
let click3 = 0;
let result = '';

const buttons = document.getElementsByClassName('button');
for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', function () {
        // ToDo: Need a global clear-at-any-stage handler
        // ToDo: Multiple digits -- only single digits supported at the moment

        // No need for clickCount
        // Simply enter digits until C or = is clicked
        // If =, take string and parse it
        // innerhtml = result variable

        // Click 1 should be the first number
        if (clickCount === 0) {
            if (this.innerHTML == parseInt(this.innerHTML)) {
                click1 = parseInt(this.innerHTML);
                clickCount++;
            }
            else {
                alert('The first click must be a number');
            }
        }

        // Click 2 should be the operator
        else if (clickCount === 1) {
            if (['+', '-', 'X', '/'].includes(this.innerHTML)) {
                click2 = this.innerHTML;
                clickCount++;
                document.getElementById('output').innerHTML = this.innerHTML;
            }
            else {
                alert('The second click must be an operator');
            }
        }

        // Click 3 should be the second number
        else if (clickCount === 2) {
            if (this.innerHTML == parseInt(this.innerHTML)) {
                click3 = parseInt(this.innerHTML);
                clickCount++;
                document.getElementById('output').innerHTML = this.innerHTML;
            }
            else {
                alert('The third click must be a number');
            }
        }

        // Once all the values have been entered
        result += this.innerHTML;
        if (this.innerHTML === "=") {
            // Craft the payload
            var request = JSON.stringify({
                Operand1: click1,
                Symbol: click2,
                Operand2: click3
            });

            // Send the data for processing server-side
            $.ajax({
                url: '/Calculator/Calculate',
                type: 'POST',
                dataType: "json",
                contentType: 'application/json',
                data: request,
                // Get the response
                success: function (data) {
                    document.getElementById('output').innerHTML = data.result;
                }
            });
        }
        else {
            document.getElementById('output').innerHTML = this.innerHTML;
        }

        // Clear functionality
        if (this.innerHTML === "C") {
            clickCount = 0;
            document.getElementById('output').innerHTML = '';
        }
    });
}