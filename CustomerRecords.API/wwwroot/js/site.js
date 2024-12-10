var _customers = [];
$(document).ready(function () {
    LoadCustomers();
});
function LoadCustomers() {
    Reset();
    $("#tblCustomer tbody").empty();

    $.get("../Customers/GetCustomers", function (customers) {
        _customers = customers;
        $.map(customers, function (customer) {
            var tempStr = "<tr>";
            tempStr += "<td>" + customer.id + "</td>";
            tempStr += "<td>" + customer.firstName + "</td>";
            tempStr += "<td>" + customer.lastName + "</td>";
            tempStr += "<td>" + customer.email + "</td>";
            tempStr += "<td>" + customer.phoneNumber + "</td>";
            tempStr += "<td>" + customer.address + "</td>";
            tempStr += "<td><button class='btn btn-primary' onclick='Edit(\"" + customer.id + "\")'>Edit</button><button class='btn btn-danger' onclick='Delete(\"" + customer.id + "\")'>Delete</button>";
            tempStr += "</tr>";
            $("#tblCustomer tbody").append(tempStr);
        });
    });
}

function Reset() {
    _customers = [];
    $(".inputField").val("");
}

function Save() {
    var firstName = $.trim($("#txtFirstName").val());
    var lastName = $.trim($("#txtLastName").val());
    var email = $.trim($("#txtEmail").val());
    var phoneNumber = $.trim($("#txtPhoneNumber").val());
    var address = $.trim($("#txtAddress").val());

    if (!firstName || !lastName || !email || !phoneNumber || !address) {
        alert("Please fill in all fields before saving!");
        return;
    }

    if (!/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)) {
        alert("Please enter a valid email address.");
        return;
    }

    if (phoneNumber.length !== 10 || !/^\d+$/.test(phoneNumber)) {
        alert("Phone number must be exactly 10 digits.");
        return;
    }

    var customer = {
        id: $.trim($("#txtCustomerId").val()),
        FirstName: firstName,
        LastName: lastName,
        Email: email,
        PhoneNumber: phoneNumber,
        Address: address
    };

    $.post("../Customers/CreateCustomer", customer, function (cust) {
        LoadCustomers();
    });
}

function Edit(customerId) {
    var customer = _customers.find(c => c.id == customerId);
    $("#txtCustomerId").val(customer.id);
    $("#txtFirstName").val(customer.firstName);
    $("#txtLastName").val(customer.lastName);
    $("#txtEmail").val(customer.email);
    $("#txtPhoneNumber").val(customer.phoneNumber);
    $("#txtAddress").val(customer.address);
}

function Delete(customerId) {
    $.ajax({
        url: "../Customers/DeleteCustomer?customerId=" + customerId,
        method: 'DELETE'
    })
        .done(function (data) {
            alert(data);
            LoadCustomers();
        });
}