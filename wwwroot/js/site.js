// Set defaults
let clickCount = 0;
let click1 = 0;
let click2 = '+';
let click3 = 0;

const buttons = document.getElementsByClassName('button');
for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', function () {
        // ToDo: Need a global clear-at-any-stage handler
        // ToDo: Multiple digits -- only single digits supported at the moment

        // Click 1 should be the first number
        if (clickCount === 0) {
            if (this.innerHTML == parseInt(this.innerHTML)) {
                click1 = parseInt(this.innerHTML);
                clickCount++
                alert('The first click was: ' + this.innerHTML);
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
                alert('The second click was: ' + this.innerHTML);
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
                alert('The third click was: ' + this.innerHTML);
            }
            else {
                alert('The third click must be a number');
            }
        }

        // Once all the values have been entered
        else if (clickCount === 3) {
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
                        alert('The result is: ' + data.result);
                        console.log(data);
                    }
                });
            }
            else {
                // Only = for final click for now
                alert('Ready to calculate -- please click the = button');
            }
        }
    });
}