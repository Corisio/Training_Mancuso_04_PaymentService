# E.4 – Mocking
 
## Problem description:  Payment service
  
Given a user wants to buy her selected items
When she submits her payment details
Then we should process her payment
  
## Acceptance criteria:
- If the user is not valid, an exception should be thrown.
- Payment should be sent to the payment gateway only when user is valid.
  
Create a class with the following signature:

```java  
public class PaymentService {
  public void processPayment(User user,
                              PaymentDetails paymentDetails) {
      // your code goes here
  }
}
```