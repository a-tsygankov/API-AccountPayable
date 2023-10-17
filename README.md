# BE-take-home-task-AndreyTsygankov


## Design spec for Account Payable scenario

### Entities
1. Vendor
2. Bill
3. Paid bill


### Data schema


### API


### Assumptions
- Authentication/authorization: system-wide, build-in MS or placeholder for JWT token 
- Idempotency (*ask Mike*): one of the operation (bill creation) is vulnarable to repeatition. Not critical for vendor operations or payments. Can be solve using system-wide service/framework or implemented by persisting form id and checking it.
- Performance (*ask Mike*): rudimental caching of vendors
- 

[link](https://github.com/plootoinc/BE-take-home-task-AndreyTsygankov/)

