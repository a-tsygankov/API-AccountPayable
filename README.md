# BE-take-home-task-AndreyTsygankov
---

## Design spec for Account Payable scenario

Design of Account Payable service is based on simpel REST API and data storage on the backaend isolated with IRepository pattern. 
Alternative options like QraphQL or direct pub/sub messaging were considered. Reasons to choose REST: data stracture is simple and entities have only a few parameters, most operations are CRUD, HTTP caching can be used, system-wide idempotency control can be (which probably wouldn't such a problem in case of GraphQL or messaging). 

Simple data structure and refernces can be easily described in relational format.  However, usage of IRepository abstraction layer allows to replace particular data storage with NoSQL database or event-sourced database like Mongo or EventStore.

Logging: MS Logging. It can be wrapped in own abstraction layer if needed. 
ORM: Drapper, EF Core

Persistance: SQLLIte, mock in-memory implementation for tests and debugging


### Entities and data schema (*ask Mike*)
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
   - AcountId: number (FK to Account.id)
   - billId: numbere (FK to Bill.Id)
   - paymentDate: Date
   - amount: number
   - paymentMethod: number (FK to PaymentMethod)

5. PaymentMethod (enum)
   - id: number
   - displayName: string



### API

GET /vendors
GET /bills?onlyPaid=true/false
GET /payments
POST /payments 
{payment info json, isSavePayment:true}
PUT /payments/<id> {isPaid:true}


#### 1. Create Payment
**Method:** POST

**Endpoint:** /api/payments

**Request Body:**
```
{
  "billIds": [1, 2, 3], // Array of bill IDs to be paid
  "paymentDetails": {
    "amount": 1000, // Total payment amount
    "debitDate": "2023-10-25", // Date when the payment will be debited
    "paymentMethod": "Bank transfer" // Payment method
  }
}
```
**Response Body:**
```
{
  "paymentId": 123, // Unique payment ID
  "status": "SUCCESS", // Status of the payment creation
  "message": "Payment created successfully" // Additional message
}
```
#### 2. Mark Bills as Paid
**Method:** PUT

**Endpoint:** /api/bills/mark-paid

**Request Body:**

```
{
  "billIds": [1, 2, 3] // Array of bill IDs to mark as paid
}
```

**Response Body:**

```
{
  "status": "SUCCESS", // Status of the bill update operation
  "message": "Bills marked as paid successfully" // Additional message
}
```

#### 3. Get Paid Bills
**Method:** GET

**Endpoint:** /api/bills/paid

**Response Body:**


```
[
  {
    "id": 1, // Bill ID
    "orderOf": "Flowers"
    "vendorId": 10, // Vendor ID
    "amount": 500, // Bill amount
    "paymentId": 123, // Associated payment ID
    "paymentMethod": 23, // Payment method
    "paymentDate": "2023-10-25" // Payment date
  },
  {
    "id": 2,
    "orderOf": "Wine"
     "vendorId": 12,
    "amount": 300,
    "paymentId": 124,
    "paymentMethod": "Email transfer",
    "paymentDate": "2023-10-26"
  }
]
```







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
- add  IMemoryCache to IUnitOfWork (this will allow to cache only vendors and methods, without caching bills and payments)
- implement view bills as query or mark bill as paid as separate method (not update)
- convert BillRM in order to keep a list of payments
- 