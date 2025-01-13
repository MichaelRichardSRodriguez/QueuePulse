document.addEventListener("DOMContentLoaded", function () {
    // Call to load data when the page is loaded.
    loadGroups();

    // Event listeners for search and filter actions
    document.getElementById("statusFilter").addEventListener("change", function () {
        loadGroups();
    });

    document.getElementById("searchButton").addEventListener("click", function () {
        document.getElementById("searchInput").focus();
        loadGroups();
    });

    document.getElementById("searchInput").addEventListener("input", function () {
        loadGroups();
    });


    function loadGroups() {
        const status = document.getElementById("statusFilter").value;
        const searchQuery = document.getElementById("searchInput").value;

        // Show loading message or spinner if required
        const tableBody = document.querySelector("#groupTable tbody");
        tableBody.innerHTML = "<tr><td colspan='3'>Loading...</td></tr>";

        // AJAX request to get data
        fetch(`/QueueGroup/GetGroups?searchQuery=${searchQuery}&statusFilter=${status}`)
            .then(response => response.json())
            .then(data => {
                renderTable(data);
            })
            .catch(error => {
                //console.error('Error fetching services:', error);
                alert(error);
                tableBody.innerHTML = "<tr><td colspan='3'>Error loading data.</td></tr>";
            });
    }

    function updateGroupStatus(itemId) {
        fetch(`/queueGroup/UpdateStatus/${itemId}`, {
            method: 'GET', // or 'POST' depending on your API requirement // Use POST since you're updating data
            headers: {
                'Content-Type': 'application/json', // Indicate the request body is JSON

                // Add any other headers if necessary (e.g., authorization tokens)
            },
        })

            .then(response => {
                if (!response.ok) {
                    throw new Error('Server responded with an error: ' + response.statusText);
                }
                return response.json(); // Assuming the response is JSON
            })
            .then(data => {
                if (data.success) {
                    toastr.success('Service status updated successfully.');
                } else {
                    toastr.error(data.message || 'Failed to update service status.');
                }

                loadGroups(); // Reload the service list or update the UI accordingly
            })
            .catch(error => {
                toastr.error('An error occurred while updating the service status: ' + error.message);
            });
    }

    function renderTable(data) {
        const tableBody = document.querySelector("#groupTable tbody");
        tableBody.innerHTML = ""; // Clear previous rows

        if (data.length === 0) {
            tableBody.innerHTML = "<tr><td colspan='3'>No group found.</td></tr>";
            return;
        }

        data.forEach(item => {
            const row = document.createElement("tr");

            // Name column
            const nameCell = document.createElement("td");
            nameCell.textContent = item.name;
            nameCell.style.fontWeight = "bold";
            nameCell.style.color = "#155E95";
            row.appendChild(nameCell);

            // Description column
            const descriptionCell = document.createElement("td");
            descriptionCell.textContent = item.description;
            row.appendChild(descriptionCell);

            // Status column
            const statusCell = document.createElement("td");
            const statusButton = document.createElement("a");
            // Determine the class based on the status
            statusButton.className = item.status.toUpperCase() === "ACTIVE" ? "btn btn-success btn-sm w-100" : "btn btn-danger btn-sm w-100";
            // Set the text content based on the status
            statusButton.textContent = item.status;

            statusCell.appendChild(statusButton);
            row.appendChild(statusCell);


            // Add SweetAlert confirmation on click
            statusButton.addEventListener('click', function (event) {
                event.preventDefault();  // Prevent default navigation

                // Show SweetAlert confirmation
                Swal.fire({
                    title: 'Are you sure?',
                    text: `Do you really want to ${item.status.toUpperCase() === "ACTIVE" ? 'DEACTIVATE' : 'ACTIVATE'} this item?`,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, change status!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        updateGroupStatus(item.id);
                        loadGroups();
                    }
                });
            });

            const isActive = item.status === "Active";

            // Actions column
            const actionsCell = document.createElement("td");
            actionsCell.innerHTML = `
                <a class="btn btn-warning btn-sm w-25 ${isActive ? '' : 'disabled'}" href="/queueGroup/Edit/${item.id}">Edit</a>
                <a class="btn btn-primary btn-sm w-25" href="/queueGroup/Details/${item.id}">Details</a>
                <a class="btn btn-danger btn-sm w-25" href="/queueGroup/Delete/${item.id}">Delete</a>
            `;

            row.appendChild(actionsCell);

            // Append row to table body
            tableBody.appendChild(row);
        });
    }
});


