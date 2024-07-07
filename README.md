# api-balance-managment
I had a test that came from a company, so I've decided to publish what I did and HOW I did


**the tests gave as instructions:**
Read and comprehend the following user story and to build a production ready .NET API that
implements the specified functionality. Your task involves creating a robust backend solution for
the Edenred application, enabling users to efficiently manage their top-up beneficiaries, explore
available top-up options, and execute top-up transactions for their UAE phone numbers.

At {{COMPANY}}, we provide financial inclusion services for underbanked employees in the
UAE where we provide each employee with a card that they receive their salary on
and a mobile application where they can see their balance and enjoy value added
services.
**User Story:**
As a user of the {{COMPANY}} mobile application, I want to top up my UAE phone numbers with
credit so I can make local phone calls.

**Acceptance Criteria:**
● The user can add a maximum of 5 active top-up beneficiaries.
● Each top-up beneficiary must have a nickname with a maximum length of 20
characters.
● The user should be able to view available top-up beneficiaries.
● The user should be able to view available top-up options (U$D 5, U$D 10, U$D 20, U$D 30,
U$D 50, U$D 75, U$D 100).
● If a user is not verified, they can top up a maximum of U$D 1,000 per calendar month per
beneficiary for security reasons.
● If a user is verified, they can top up a maximum of U$D 500 per calendar month per
beneficiary.
● The user can top up multiple beneficiaries but is limited to a total of U$D 3,000 per month
for all beneficiaries.
● A charge of U$D 1 should be applied for every top-up transaction.
● The user can only top up with an amount equal to or less than their balance which will be
retrieved from an external HTTP service.
● The user's balance should be debited first before the top-up transaction is attempted.
