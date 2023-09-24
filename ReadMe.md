### Assignment Instructions

- **Time to complete:** 10 minutes
- **Number of student solutions:** 161

**Requirement:**

Imagine a banking application. Create an ASP.NET Core Web Application that serves bank account details based on the
request path.

Consider the following hard-coded bank account details:

- `accountNumber = 1001`
- `accountHolderName = "Example Name"`
- `currentBalance = 5000`

You can store these details as an anonymous object. For example: `new { property1 = value, property2 = value }`

**Example #1:**

If you receive an HTTP GET request at the path "/" (default route), it should return a welcome message with a status
code of 200.

- **Request URL:** /
- **Request Method:** GET
- **Response Status Code:** 200
- **Response Body (output):**

Welcome to the Best Bank

**Example #2:**

If you receive an HTTP GET request at the path "/account-details", it should return all the details of the bank account
as a JSON format response with a status code of 200.

- **Request URL:** /account-details
- **Request Method:** GET
- **Response Status Code:** 200
- **Response Body (output):**

json
{
"accountNumber": 1001,
"accountHolderName": "Example Name",
"currentBalance": 5000
}
![sample3.png](wwwroot%2Fsample3.png)

**Example #3:**

If you receive an HTTP GET request at the path "/account-statement", it should return a dummy PDF file (assumed as a
bank statement) as a response with a status code of 200.

- **Request URL:** /account-statement
- **Request Method:** GET
- **Response Status Code:** 200
- **Response Body (output):**
  [some dummy PDF file]

**Example #4:**

If you receive an HTTP GET request at the path "/get-current-balance/{accountNumber}", it should return the
corresponding current balance value as a response with a status code of 200. The "accountNumber" should be an integer
value and should be equal to "1001".

- **Request URL:** /get-current-balance/1001
- **Request Method:** GET
- **Response Status Code:** 200
- **Response Body (output):**

![sample 4.png](wwwroot%2Fsample%204.png)

**Example #5:**

If you receive an HTTP GET request at the path "/get-current-balance/", and the "accountNumber" is not supplied, it
should return an HTTP 404 response.

- **Request URL:** /get-current-balance
- **Request Method:** GET
- **Response Status Code:** 404
- **Response Body (output):**

Account Number should be supplied

**Example #6:**

If you receive an HTTP GET request at the path "/get-current-balance/{accountNumber}", and the "accountNumber" is not
equal to '1001', it should return an HTTP 400 response.

- **Request URL:** /get-current-balance/10
- **Request Method:** GET
- **Response Status Code:** 400
- **Response Body (output):**

![sample 5.png](wwwroot%2Fsample%205.png)

**Route Constraints:**

The "accountNumber" parameter should be an integer value.

**Instructions:**

- Create controller(s) with attribute routing.
- Use essential route parameters and route constraints.
- Use parameter validation when necessary.
- Return `ContentResult`, `JsonResult`, `FileResult`, and other status code results when necessary.
- Return the appropriate HTTP response status code based on the examples provided above.
