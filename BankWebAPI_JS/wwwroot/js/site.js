// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadView(status) {
    var apiUrl = '/api/login/defaultview';
    if (status === "authview")
        apiUrl = '/api/login/authview';
    if (status === "error")
        apiUrl = '/api/login/error';
    if (status === "about")
        apiUrl = '/api/about/view';
    if (status === "logout")
        apiUrl = '/api/logout';
    if (status === "information") {
        apiUrl = '/api/information/view';
        getUserDatas();
    }
    if (status === "account") {
        apiUrl = '/api/account/view';
        getAccBalance();
    }
    if (status === "transaction") { 
        apiUrl = '/api/transaction/view';
        getTransactionByFromId();
    }
    if (status === "transfer")
        apiUrl = '/api/transfer/view';

    console.log("Hello " + apiUrl);

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            // Handle the data from the API
            document.getElementById('main').innerHTML = data;
            if (status === "logout") {
                document.getElementById('LogoutButton').style.display = "none";
                document.getElementById('InformationButton').style.display = "none";
                document.getElementById('AccountButton').style.display = "none";
                document.getElementById('TransactionButton').style.display = "none";
                document.getElementById('TransferButton').style.display = "none";
            }
        })
        .catch(error => {
            // Handle any errors that occurred during the fetch
            console.error('Fetch error:', error);
        });

}


function performAuth() {

    var name = document.getElementById('SName').value;
    var password = document.getElementById('SPass').value;
    var data = {
        UserName: name,
        PassWord: password
    };
    console.error(data);
    const apiUrl = '/api/login/auth';

    const headers = {
        'Content-Type': 'application/json', // Specify the content type as JSON if you're sending JSON data
        // Add any other headers you need here
    };

    const requestOptions = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data) // Convert the data object to a JSON string
    };

    fetch(apiUrl, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Handle the data from the API
            const jsonObject = data;
            if (jsonObject.login) {
                loadView("authview");
                document.getElementById('LogoutButton').style.display = "block";
                document.getElementById('InformationButton').style.display = "block";
                document.getElementById('AccountButton').style.display = "block";
                document.getElementById('TransactionButton').style.display = "block";
                document.getElementById('TransferButton').style.display = "block";
            }
            else {
                loadView("error");
            }

        })
        .catch(error => {
            // Handle any errors that occurred during the fetch
            console.error('Fetch error:', error);
        });

}

function getAccBalance(){
    var sessionId = getCookie("SessionID");
    if (!sessionId) {
        return;
    }

    $.ajax({
        url: '/api/Account/get/' + + sessionId, // Replace '123' with the actual account number
        type: 'GET',
        success: function (data) {
            // Assuming the returned data is in JSON format
            $('#AcctNo').val(data.acctNo);
            $('#AcctBal').val(data.acctBal);
        },
        error: function () {
            alert('User not found');
        }
    });
}

function getUserDatas() {
    var sessionId = getCookie("SessionID");
    if (!sessionId) {
        return;
    }

    $.ajax({
        url: '/api/User/getacc/' + + sessionId, 
        type: 'GET',
        success: function (data) {
            // Assuming the returned data is in JSON format
            $('#SName').val(data.userName);
            $('#SPass').val(data.password);
            $('#SEmail').val(data.email);
            $('#SPhone').val(data.phoneNumber);
        },
        error: function () {
            alert('User not found');
        }
    });
}

function getTransactionByFromId() {
    var sessionId = getCookie("SessionID");
    if (!sessionId) {
        return;
    }

    $.ajax({
        url: '/api/Transaction/getbyfromid/' + + sessionId, // Replace '123' with the actual account number
        type: 'GET',
        success: function (data) {
            // Assuming the returned data is an array of transactions
            var tableBody = $("#transactionTableBody");
            tableBody.empty(); // Clear existing rows

            data.forEach(function (transaction) {
                var row = "<tr>" +
                    "<td>" + transaction.transId + "</td>" +
                    "<td>" + transaction.fromId + "</td>" +
                    "<td>" + transaction.toId + "</td>" +
                    "<td>" + transaction.bal + "</td>" +
                    "<td>" + "Hello" + "</td>" +
                    "<td>" + "12/12/2002" + "</td>" +
                    "</tr>";
                tableBody.append(row);
            });
        },
        error: function () {
            alert('Error fetching transaction data');
        }
    });
}

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) {
        return parts.pop().split(";").shift();
    }
    return null;
}

function updateUserData() {
    var id = getCookie("SessionID");
    var userName = $("#SName").val();
    var password = $("#SPass").val();
    var email = $("#SEmail").val();
    var phoneNumber = $("#SPhone").val();

    var user = {
        "UserName": userName,
        "Password": password,
        "Email": email,
        "PhoneNumber": phoneNumber
    };

    // Send AJAX request to update user data
    $.ajax({
        url: '/api/User/update/' + + userName,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(user),
        success: function (data) {
            alert("Successfully updated");
        },
        error: function () {
            alert("Could not update");
        }
    });
}   



document.addEventListener("DOMContentLoaded", loadView);
/*
const loginButton = document.getElementById('LoginButton');
loginButton.addEventListener('click', loadView);

const aboutButton = document.getElementById('AboutButton');
aboutButton.addEventListener('click', loadView("about"));

const logoutButton = document.getElementById('LogoutButton');
logoutButton.addEventListener('click', loadView("logout"));*/




