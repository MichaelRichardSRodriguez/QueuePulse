

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("nextqueueButton").addEventListener("click", NextQueueButtonClick);
    document.getElementById("recallButton").addEventListener("click", RecallButtonClick);
    document.getElementById("processedButton").addEventListener("click", ProcessedButtonClick);
    document.getElementById("transferButton").addEventListener("click", TransferButtonClick);
    document.getElementById("referButton").addEventListener("click", ReferButtonClick);
    document.getElementById("statusButton").addEventListener("click", OffLineOnlineButtonClick);
});

function NextQueueButtonClick() {

    var anchorProcessed = document.getElementById("processedButton");
    var anchorRecall = document.getElementById("recallButton");
    var anchorNextQueue = document.getElementById("nextqueueButton");
    var anchorStatus = document.getElementById("statusButton");
    var anchorTransfer = document.getElementById("transferButton");
    var anchorRefer = document.getElementById("referButton");
    var currentWorkstation = "Window 1";

    if (anchorProcessed) {
        anchorProcessed.classList.remove("disabled", "btn-outlined-info");
        anchorProcessed.classList.add("btn", "btn-info", "mb-1");
    }

    if (anchorNextQueue) {
        anchorNextQueue.classList.remove("btn-info");
        anchorNextQueue.classList.add("btn", "btn-outlined-info", "disabled");
    }

    if (anchorRecall) {
        anchorRecall.classList.remove("disabled", "btn-outlined-info");
        anchorRecall.classList.add("btn", "btn-info", "mb-1");
    }

    if (anchorTransfer) {
        anchorTransfer.classList.remove("disabled", "btn-outlined-info");
        anchorTransfer.classList.add("btn", "btn-info", "mb-1");
    }

    if (anchorRefer) {
        anchorRefer.classList.remove("btn-outlined-info", "disabled",);
        anchorRefer.classList.add("btn", "btn-info", "mb-1");
    }

    if (anchorStatus) {
        anchorStatus.classList.add("disabled");
    }

    getQueueItemName(currentWorkstation);
    resetTimer();

}


function getQueueItemName(currentWorkstation) {

    document.getElementById('queueItemNameLabel').textContent = '';
    var url = '/Queue/GetNextQueueItem?currentWorkstation=' + encodeURIComponent(currentWorkstation);    /*'@Url.Action("GetNextQueueItem", "Queue")';*/

    // Perform the fetch call to the GetQueueItemName action in the controller
    fetch(url) // The controller action URL
        .then(response => response.json()) // Parse the JSON response
        .then(data => {
            if (data.success) {
                // Display the name in the #queueItemName element
                document.getElementById('queueItemNameLabel').textContent = data.queueNo;
                document.getElementById('queueItemId').value = data.id;

                reloadQueueItems();
            } else {
                // Show error message
                alert(data.message);
            }
        })
        .catch(() => {
            // Handle any errors that may occur
            document.getElementById('queueItemNameLabel').textContent = 'An error occurred while fetching data.';
        });


}

function OffLineOnlineButtonClick() {
    var anchorStatus = document.getElementById("statusButton");
    var anchorNextQueue = document.getElementById("nextqueueButton");

    if (anchorStatus) {
        if (anchorStatus.textContent.toLowerCase() == "offline") {
            anchorStatus.classList.remove("btn-danger");
            anchorStatus.classList.add("btn-success");
            anchorStatus.textContent = "Online";

            if (anchorNextQueue) {
                anchorNextQueue.classList.remove("disabled", "btn-outlined-info");
                anchorNextQueue.classList.add("btn", "btn-info", "mb-1");
            }
        }
        else {
            anchorStatus.classList.remove("btn-success");
            anchorStatus.classList.add("btn-danger");
            anchorStatus.textContent = "Offline";

            if (anchorNextQueue) {
                anchorNextQueue.classList.remove("btn-info");
                anchorNextQueue.classList.add("btn", "btn-outlined-info", "disabled");
            }
        }

    }
}

function TransferButtonClick() {

    var anchorNextQueue = document.getElementById("nextqueueButton");
    var anchorProcessed = document.getElementById("processedButton");
    var anchorRecall = document.getElementById("recallButton");
    var anchorStatus = document.getElementById("statusButton");
    var anchorTransfer = document.getElementById("transferButton");
    var anchorRefer = document.getElementById("referButton");

    if (anchorNextQueue) {
        anchorNextQueue.classList.remove("disabled", "btn-outlined-info");
        anchorNextQueue.classList.add("btn", "btn-info", "mb-1");
    }
    if (anchorProcessed) {
        anchorProcessed.classList.remove("btn-info");
        anchorProcessed.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorRecall) {
        anchorRecall.classList.remove("btn-info");
        anchorRecall.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorTransfer) {
        anchorTransfer.classList.remove("btn-info");
        anchorTransfer.classList.add("btn", "btn-outlined-info", "disabled");
    }

    if (anchorRefer) {
        anchorRefer.classList.remove("btn-info");
        anchorRefer.classList.add("btn", "btn-outlined-info", "disabled");
    }

    if (anchorStatus) {
        anchorStatus.classList.remove("disabled");
    }

    alert("Transfer Button Clicked!!!");
}
function ReferButtonClick() {
    var anchorNextQueue = document.getElementById("nextqueueButton");
    var anchorProcessed = document.getElementById("processedButton");
    var anchorRecall = document.getElementById("recallButton");
    var anchorStatus = document.getElementById("statusButton");
    var anchorTransfer = document.getElementById("transferButton");
    var anchorRefer = document.getElementById("referButton");

    if (anchorNextQueue) {
        anchorNextQueue.classList.remove("disabled", "btn-outlined-info");
        anchorNextQueue.classList.add("btn", "btn-info", "mb-1");
    }
    if (anchorProcessed) {
        anchorProcessed.classList.remove("btn-info");
        anchorProcessed.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorRecall) {
        anchorRecall.classList.remove("btn-info");
        anchorRecall.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorTransfer) {
        anchorTransfer.classList.remove("btn-info");
        anchorTransfer.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorRefer) {
        anchorRefer.classList.remove("btn-info");
        anchorRefer.classList.add("btn", "btn-outlined-info", "disabled");
    }

    if (anchorStatus) {
        anchorStatus.classList.remove("disabled");
    }

    alert("Refer Button Clicked!!!");
}

function RecallButtonClick() {
    recallQueueItem();
}

function ProcessedButtonClick() {

    var anchorNextQueue = document.getElementById("nextqueueButton");
    var anchorProcessed = document.getElementById("processedButton");
    var anchorRecall = document.getElementById("recallButton");
    var anchorStatus = document.getElementById("statusButton");
    var anchorTransfer = document.getElementById("transferButton");
    var anchorRefer = document.getElementById("referButton");

    if (anchorNextQueue) {
        anchorNextQueue.classList.remove("disabled", "btn-outlined-info");
        anchorNextQueue.classList.add("btn", "btn-info", "mb-1");
    }
    if (anchorProcessed) {
        anchorProcessed.classList.remove("btn-info");
        anchorProcessed.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorRecall) {
        anchorRecall.classList.remove("btn-info");
        anchorRecall.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorTransfer) {
        anchorTransfer.classList.remove("btn-info");
        anchorTransfer.classList.add("btn", "btn-outlined-info", "disabled");
    }

    if (anchorRefer) {
        anchorRefer.classList.remove("btn-info");
        anchorRefer.classList.add("btn", "btn-outlined-info", "disabled");
    }
    if (anchorStatus) {
        anchorStatus.classList.remove("disabled");
    }


    markAsCompleted();
    stopTimer();

}

// This function is called when the page loads to fetch and display the queue items
function reloadQueueItems() {

    // Make the API call to fetch the data
    // fetch('/Queue/GetAllQueueItems')  // Adjust URL as necessary
    // 	.then(response => response.json())  // Convert response to JSON
    // 	.then(queueItems => {
    // 		// Get the tbody element to insert rows into
    // 		const tableBody = document.getElementById('queueList');
    // 		tableBody.innerHTML = ''; // Clear any existing rows (if any)

    // 		// Force a reflow to ensure the DOM updates immediately before the new rows are added
    // 		document.body.offsetHeight;  // Trigger a reflow to make sure styles are recalculated

    // 		// Loop through the fetched queue items and create table rows
    // 		queueItems.forEach(function (item) {
    // 			const row = document.createElement('tr');
    // 			row.classList.add('table-light');

    // 			// Add table cells (td) for each property of the queue item
    // 			row.innerHTML = `
    // 														<td>${item.name}</td>
    // 														<td>${new Date(item.dateCreated).toLocaleString()}</td>
    // 														<td>${item.dateProcessed ? new Date(item.dateProcessed).toLocaleString() : ''}</td>
    // 														<td>${item.status}</td>
    // 													`;

    // 			// Append the row to the table body
    // 			tableBody.appendChild(row);
    // 		});
    // 	})
    // 	.catch(error => {
    // 		console.error('Error fetching queue items:', error);
    // 	});

    //REVISED CODE USING CARDS
    fetch('/Queue/GetAllQueueItems')  // Adjust URL as necessary
        .then(response => response.json())  // Convert response to JSON
        .then(queueItems => {
            // Get the container element to insert the cards
            const cardContainer = document.getElementById('queueCards');
            cardContainer.innerHTML = ''; // Clear any existing cards

            // Force a reflow to ensure the DOM updates immediately before the new rows are added
            document.body.offsetHeight;  // Trigger a reflow to make sure styles are recalculated

            // Function to map status to corresponding style class
            function getStatusStyle(status) {
                switch (status.toUpperCase()) {
                    case 'IN QUEUE':
                        return 'status-pending';
                    case 'PROCESSING':
                        return 'status-in-progress';
                    case 'COMPLETED':
                        return 'status-completed';
                    default:
                        return '';  // Default class if no match
                }
            }

            // Loop through the fetched queue items and create cards
            queueItems.forEach(function (item) {
                const card = document.createElement('div');
                card.classList.add('card-custom');

                // Create the inner HTML for each card
                card.innerHTML = `
					<div class="card-body-custom">
						<h4 class="card-title" style="color: #1D3557"><strong>${item.queueNo}</strong></h4>
						<div class="row">
							<div class="col-8">
								<h6 class="card-title mb-2 ${getStatusStyle(item.status)}">
								<strong>${item.status}</strong>
								</h6>
							</div>
							<div class="col-4">
								<a href="#" class="btn btn-outline-info btn-sm">View</a>
							</div>
						</div>
					</div>
				  `;

                // Append the card to the container
                cardContainer.appendChild(card);
            });

            scrollToBottom();

        })
        .catch(error => {
            console.error('Error fetching queue items:', error);
        });

}

// Function to handle marking a queue item as completed
function markAsCompleted() {

    var id = document.getElementById("queueItemId").value;

    $.ajax({
        url: '/Queue/MarkAsCompleted', // Adjust this URL to match your route
        type: 'POST',
        data: { id: id },
        success: function (response) {
            if (response.success) {
                reloadQueueItems();
            } else {
                alert('Failed to mark as completed: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            alert('An error occurred: ' + error);
        }
    });

}

// Function to call the Recall action when the Recall button is clicked
function recallQueueItem() {
    fetch('/Queue/RecallQueueItem')
        .then(response => response.json())
        .then(data => {
            // The page won't change; just plays the sound
        })
        .catch(error => console.error('Error recalling queue item:', error));
}

//********************* DYNAMIC TIME ******************/

let seconds = 0;  // Time counter in seconds
let timer;  // Timer variable

// Function to update the time display
function updateTimeDisplay() {
    const hours = Math.floor(seconds / 3600);  // Calculate hours
    const minutes = Math.floor((seconds % 3600) / 60);  // Calculate minutes
    const remainingSeconds = seconds % 60;  // Calculate seconds

    // Update the display in the format hh:mm:ss
    document.getElementById("timeDisplay").textContent = `${padZero(hours)}:${padZero(minutes)}:${padZero(remainingSeconds)}`;
}

// Function to pad time values with a leading zero (e.g., 01, 09)
function padZero(num) {
    return num < 10 ? "0" + num : num;
}

// Start the timer
function startTimer() {
    if (!timer) { // Start timer only if it's not already running
        timer = setInterval(() => {
            seconds++;
            updateTimeDisplay();
        }, 1000);
    }
}

// Reset the timer
function resetTimer() {
    clearInterval(timer);  // Stop the timer
    seconds = 0;  // Reset seconds
    updateTimeDisplay();  // Update display to show 00:00:00
    startTimer();  // Restart the timer
}

// Stop the timer
function stopTimer() {
    clearInterval(timer);  // Stop the timer
    timer = null;  // Reset the timer variable so it can be started again later
}

// Function to scroll the container to the bottom
function scrollToBottom() {
    const container = document.getElementById('queueCards'); // Replace with your container's ID if different
    container.scrollTop = container.scrollHeight;
}