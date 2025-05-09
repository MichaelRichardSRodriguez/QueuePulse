﻿document.addEventListener("DOMContentLoaded", function () {
    // Call to load data when the page is loaded.
    loadDepartments();

    // Event listeners for search and filter actions
    document.getElementById("statusFilter").addEventListener("change", function () {
        loadDepartments();
    });

    document.getElementById("searchButton").addEventListener("click", function () {
        document.getElementById("searchInput").focus();
        loadDepartments();
    });

    document.getElementById("searchInput").addEventListener("input", function () {
        loadDepartments();
    });

    //document.getElementById("recordPerPage").addEventListener("change", function () {
    //    loadDepartments();
    //});


    //document.getElementById("searchInput").addEventListener("keydown", function (event) {

    //    var inputValue = document.getElementById("searchInput").value;

    //    if (event.key === "Enter") {
    //        if (inputValue.trim() !== "") {
    //            document.getElementById("searchButton").click();
    //        }
    //    }
    //    else {
    //        if (inputValue.trim() === "") {
    //            loadDepartments();
    //        }
    //    }

    //});

    function loadDepartments() {
        const status = document.getElementById("statusFilter").value;
        const searchQuery = document.getElementById("searchInput").value;

        //Pagination
        const recordPerPageElement = document.getElementById("recordPerPage");
        const pageNo = 1;

        let recordPerPage;
        if (recordPerPageElement) {
            recordPerPage = recordPerPageElement.value
        }
        else {
            recordPerPage = 10;
        }

        // Show loading message or spinner if required
        const tableBody = document.querySelector("#departmentTable tbody");
        tableBody.innerHTML = "<tr><td colspan='4'>Loading...</td></tr>";

        // AJAX request to get data
        fetch(`/admin/department/getDepartments?searchQuery=${searchQuery}&statusFilter=${status}&recordPerPage=${recordPerPage}`) //&pageNo=${pageNo}`)
            .then(response => response.json())
            .then(data => {
                renderTable(data);
            })
            .catch(error => {
                console.error('Error fetching departments:', error);
                tableBody.innerHTML = "<tr><td colspan='4'>Error loading data.</td></tr>";
            });
    }

    function updateDepartmentStatus(itemId) {
        fetch(`/admin/department/UpdateStatus/${itemId}`, {
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
                    toastr.success('Department status updated successfully.');
                } else {
                    toastr.error(data.message || 'Failed to update department status.');
                }

                loadDepartments(); // Reload the department list or update the UI accordingly
            })
            .catch(error => {
                toastr.error('An error occurred while updating the department status: ' + error.message);
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
                        // If confirmed, navigate to the URL
                        //window.location.href = "/department/UpdateStatus/" + item.id;  // Redirect to the deactivation/activation URL

                        updateDepartmentStatus(item.id);
                        loadDepartments();
                    }
                });
            });

            const isActive = item.status === "Active";

            // Actions column
            const actionsCell = document.createElement("td");
            actionsCell.innerHTML = `
                <a class="btn btn-warning btn-sm w-30 ${isActive ? '' : 'disabled'}" href="/admin/department/Edit/${item.id}">
                    <i class="bi bi-pencil-square"></i>
                </a>
                <a class="btn btn-primary btn-sm w-30" href="/admin/department/Details/${item.id}">
                    <i class="bi bi-info-square-fill"></i>
                </a>
                <a class="btn btn-danger btn-sm w-30" href="/admin/department/Delete/${ item.id }">
                    <i class="bi bi-trash-fill"></i>
                 </a>
            `;

            row.appendChild(actionsCell);

            // Append row to table body
            tableBody.appendChild(row);
        });
    }
});


