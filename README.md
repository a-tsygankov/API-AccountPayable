# BE-take-home-task-AndreyTsygankov
---

## Design spec for Account Payable scenario

Design of Account Payable service is based on simpel REST API and data storage on the backaend isolated with IRepository pattern. 
Alternative options like QraphQL or direct pub/sub messaging were considered. Reasons to choose REST: data stracture is simple and entities have only a few parameters, most operations are CRUD, HTTP caching can be used, system-wide idempotency control can be used. 

Simple data structure and references can be easily described in relational format.  However, usage of IRepository abstraction layer allows to replace particular data storage with NoSQL database or event-sourced database like Mongo or EventStore.

Logging: MS Logging. It can be wrapped in own abstraction layer if needed. 

ORM: I used Drapper for simplicity, EF Core is reasonable alternative.

Persistent logging: Any action with financial system needs to be logged due to security concerns. Typical method for this is to send corresponding events to Kafka or EventStoreDB. So, even with admin privileges these messages couldn't be modified.

Persistance: SQLIte, mock in-memory implementation for tests and debugging


### Data schema (*ask Mike*)
#### Entities
1. Vendor 
   - id: number
   - displayName: string
2. Acount 
   - id: number
   - displayName: string
3. Bill
    - id: number
    - orderOf: string
    - vendorId: number (FK to Vendor.id)
    - acountId: number (FK to Account.id)
    - amount: number
    - dueDate: Date
    - paid: bool
4. Payment 
   - id: number 
   - acountId: number (FK to Account.id)
   - billId: numbere (FK to Bill.Id)
   - paymentDate: Date
   - amount: number
   - paymentMethod: number (FK to PaymentMethod)

5. PaymentMethod (enum)
   - id: number
   - displayName: string
#### Read models

- **BillRM** (describe bill with all the display names)
```
  {
    "id": 1,
    "orderOf": "Car lease",
    "accountId": 1,
    "vendorName": "Ferrari",
    "dueDate": "2023-10-24T00:00:00-04:00",
    "paid": false,
    "amount": 2000,
    "payments": [
      {
        "id": 1,
        "accountId": 1,
        "billId": 1,
        "vendorName": "Ferrari",
        "orderOf": "Car lease",
        "paymentMethodName": "Direct Deposit",
        "amount": 2000,
        "paymentDate": "2023-10-23T00:00:00-04:00"
      }
    ]
  }
```
- **PaymentRM** (describes payment read model)
```
      {
        "id": 2,
        "accountId": 1,
        "billId": 2,
        "vendorName": "Apple",
        "orderOf": "iPhone 17 pro",
        "paymentMethodName": "Credit Card",
        "amount": 100,
        "paymentDate": "2023-10-20T00:00:00-04:00"
      }
```

### API

#### Main methods:
 - **GET, endpoint: /api/v1/accounts/{accountId}/bills/query:** retrieves list of bills in form of read models according to querey parameters provided (accountId, vendorId, isPaid). If parameter is not provided then it's getting ignored in query.
 - **POST, endpoint /api/v1/accounts/{accountId}/bills/mark-paid:** marks list of bills paid if total amount of mayments matches bill's amount. 
 - **POST, endpoint /api/v1/accounts/{accountId}/payments:** payment Create method with parameters (accountId, billId, amount, paymentMethodId). PaymentDate set to current. If similar payment made within an hour is found in repository then idempotency check failed. This check is not required if we have a system-wide check for idempotency. 


### Implementation details

- REST 
- standard MS logging

### Testing

Unit tests for each layer to ensure functionality and data integrity.
Integration tests to validate interactions between layers.
End-to-end tests using Swagger UI to simulate user interactions.


### Assumptions
- Authentication/authorization: system-wide, build-in MS or placeholder for JWT token 
- Idempotency (*ask Mike*): one of the operation (bill creation) is vulnarable to repeatition. Not critical for vendor operations or payment methods. Can be solve by using system-wide service/framework or implemented by persisting form id and checking it.
- Performance (*ask Mike*): rudimental caching of vendors
- Completeness of API (*ask Mike*): some entities like Vendor need to have full CRUD implementation but this is probabaly out of scope here
- Security: all operations must be logged to persisted storage (Kafka, EVentStoreDB)
- Concurrent changes conflicts (*ask MIke*): every entity might have an updateCounter field which increments on modifications. This will allow to resolve issues with concurrent changes by comparing updateCount of current object with modification. 
- Marking bill as paid and processing payment (*ask Mike*): considered as separate operations. I.e, bill can be splitted and paid by two different methods. This approach requires extra validation when marking bill as paid.
- Payment Methods (*ask Mike*) entity may represent either system-wide  or specific for a particular account holder. Extra details do not add much logic to this implementation. So I implemented it as a simple enum without much parameters.
- Switching ids to integers for simplicity

[link](https://github.com/plootoinc/BE-take-home-task-AndreyTsygankov/)


## todo

- implement CancellationToken on every async method
- add  IMemoryCache to IUnitOfWork (this will allow to choose what to cache)
- implement view bills as query or mark bill as paid as separate method (not update)
- convert BillRM in order to keep a list of payments
- add Swagger UI pages to repo