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

**The Idea**

So... as you can see, not that much of context or decisions, just a clear objective task with no base structure (maybe that's in to test me).
Then what I've got from the interviewer was: take your time, all the info you'll need (and the only info you'll get) is on this doc up above.
But, let's get this from a diferent point of view (more technical) just to understand.

They have a mobile app, that may or may not have a monolith as a back-end application, and now they need this project...
SO... what about security? do they use integrated authentication? do they use shared cache? anything?
Since I was alone with only this info, I had to come from the principle tha EVERY SINGLE THING THAT I DON'T KNOW ON THE PROJECT, I'LL HAVE TO CONSIDER THE BEST AND MOST SCALABLE WAY POSSIBLE. So no problems will come furthermore...

**The execution**

Starting with this, I presumed this project had a DDD archtecture with a REDIS cache to manage the authenticated users (and since in some tasks of the test they say that I'll need to consume some endpoints, that will help a lot).
So I began creating a API project on hexagonal archtecture, on the domain models:
USER
BENEFICIARY

The user would be user table to store all users from this company, and the beneficiary would be a relational table to get 1 User : N beneficiaries.
But as I said, I had to make the most scalable as possible (since no background context was given), so focusing on a good ratio between performance x maintenance,
I've decided to develop a CQRS followed by a fluent assertion validation, so any business rule or business validation be encapsulated.

I've also presumed that a lot of this transactions would have to tracked, so I've came on my own to create the domain model: USERFINANCIALHISTORY
Keeping not just the action, but the balance change as well. (and since it's modeled with a HistoryType, it's scalable to add as many changes as needed to that user and balance)

At this point, I was pretty happy with my structure, but when I saw all those changes and power onto manipulating accounts, I asked myself how to manage it since NO CONTEXT WAS GIVEN...
So then I came with what I know...

So I've planed on starting with Roles for administrator (wich will have more power to add users, balance and etc...)
And User role, to manage just their own beneficiaries.

Since everything was suposed to be as scalable as possible, I've also created a SystemConfiguration domain model, where any admin could change the values being used on the rules of the software at anytime on real time.

**Security**

I've persisted the token authentication with redis on sending requests to get the current balance of the user (SINCE its a user operation, the USER should send his credentials to autentication), since no context was given I came out of the blue with that, **but just in case of security issues, ANY TOKEN, CERTIFICATE, ETC... can be injected in ExternalServicesInjection**

Since no context was given on secutiry as well, I came out from the principle that **all info of the infraestructure was about being TOTALLY HELD BY INFRAESTRUCTURE**, but how?
The answer is AWS services, using AWS CLI, the developer MUST have access to secrets manager of the current environment he is developing, so he can get any key from that.
Database connections, API keys and routes, user roles (since I've presumed there is another service managing that), ALL THOSE INFRESTRUCTURE INFORMATION are held, maintained and created by infraestructure team, so at anytime the API CURRENT BALANCE team need to move the API to another machine, change the DNS or anything, the service don't need to enter on maintenance, just upload the new service and point the new secrets and that's IT.

this also secures that any bad intensionated person with the code in hands would not have the critical access to our databases, api's or anything that actually is sensible.
