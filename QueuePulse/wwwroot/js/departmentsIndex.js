document.addEventListener("DOMContentLoaded", function () {
    // Call to load data when the page is loaded.
    loadDepartments();

    // Event listeners for search and filter actions
    document.getElementById("statusFilter").addEventListener("change", function () {
        loadDepartments();
    });


    document.getElementById("searchInput").addEventListener("input", function () {
        loadDepartments();
    });


    function loadDepartments() {
        const status = document.getElementById("statusFilter").value;
        const searchQuery = document.getElementById("searchInput").value;

        // Show loading message or spinner if required
        const tableBody = document.querySelector("#departmentTable tbody");
        tableBody.innerHTML = "<tr><td colspan='4'>Loading...</td></tr>";

        // AJAX request to get data
        fetch(`/Department/GetDepartments?searchQuery=${searchQuery}&statusFilter=${status}`)
            .then(response => response.json())
            .then(data => {
                renderTable(data);
            })
            .catch(error => {
                console.error('Error fetching departments:', error);
                tableBody.innerHTML = "<tr><td colspan='4'>Error loading data.</td></tr>";
            });
    }

    function renderTable(data) {
        const tableBody = document.querySelector("#departmentTable tbody");
        tableBody.innerHTML = ""; // Clear previous rows

        if (data.length === 0) {
            tableBody.innerHTML = "<tr><td colspan='4'>No departments found.</td></tr>";
            return;
        }

        data.forEach(item => {
            const row = document.createElement("tr");

            // Name column
            const nameCell = document.createElement("td");
            nameCell.textContent = item.name;
            row.appendChild(nameCell);

            // Description column
            const descriptionCell = document.createElement("td");
            descriptionCell.textContent = item.description;
            row.appendChild(descriptionCell);

            // Status column
            const statusCell = document.createElement("td");
            const statusButton = document.createElement("a");
            // Determine the class based on the status
            statusButton.className = item.status.toUpperCase() === "ACTIVE" ? "btn btn-success btn-sm" : "btn btn-danger btn-sm";
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
                        // If confirmed, navigate to the URL
                        window.location.href = "/department/UpdateStatus/" + item.id;  // Redirect to the deactivation/activation URL
                        //loadDepartments();
                    }
                });
            });


            // Actions column
            const actionsCell = document.createElement("td");
            actionsCell.innerHTML = `
                <a class="btn btn-warning btn-sm" href="/department/Edit/${item.id}">Edit</a>
                <a class="btn btn-primary btn-sm" href="/department/Details/${item.id}">Details</a>
                <a class="btn btn-danger btn-sm" href="/department/Delete/${item.id}">Delete</a>
            `;
            row.appendChild(actionsCell);

            // Append row to table body
            tableBody.appendChild(row);
        });
    }
});