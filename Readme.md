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



## Sandro's solution

```java
package com.codurance.craftingcode.exercise_04_make_payment_mock;
 
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.runners.MockitoJUnitRunner;
 
import static org.assertj.core.api.Assertions.assertThatExceptionOfType;
import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyZeroInteractions;
 
@RunWith(MockitoJUnitRunner.class)
public class PaymentServiceShould {
 
    private static final User USER = new User();
    private static final User INVALID_USER = new User();
    private static final PaymentDetails PAYMENT_DETAILS = new PaymentDetails();
 
    @Mock UserValidator userValidator;
    @Mock PaymentGateway paymentGateway;
 
    private PaymentService paymentService;
 
    @Before
    public void initialise() {
        paymentService = new PaymentService(userValidator, paymentGateway);
    }
 
    @Test public void
    throw_exception_when_user_is_invalid() {
        given(userValidator.validate(INVALID_USER)).willReturn(false);
 
        assertThatExceptionOfType(InvalidUserException.class)
                .isThrownBy(() -> paymentService.processPayment(INVALID_USER, PAYMENT_DETAILS));
 
        verify(userValidator).validate(INVALID_USER);
    }
 
    @Test public void
    not_invoke_payment_gateway_when_user_is_invalid() throws InvalidUserException {
        given(userValidator.validate(INVALID_USER)).willReturn(false);
 
        assertThatExceptionOfType(InvalidUserException.class)
                .isThrownBy(() -> paymentService.processPayment(INVALID_USER, PAYMENT_DETAILS));
 
        verifyZeroInteractions(paymentGateway);
    }
 
    @Test public void
    process_payment_details_when_user_is_valid() throws InvalidUserException {
        given(userValidator.validate(USER)).willReturn(true);
 
        paymentService.processPayment(USER, PAYMENT_DETAILS);
 
        verify(paymentGateway).payWith(PAYMENT_DETAILS);
    }
 
}
 
 
package com.codurance.craftingcode.exercise_04_make_payment_mock;
 
public class PaymentService {
 
    private UserValidator userValidator;
    private PaymentGateway paymentGateway;
 
    public PaymentService(UserValidator userValidator,
                          PaymentGateway paymentGateway) {
        this.userValidator = userValidator;
        this.paymentGateway = paymentGateway;
    }
 
    public void processPayment(User user,
                               PaymentDetails paymentDetails) throws InvalidUserException {
        if (!userValidator.validate(user)) {
            throw new InvalidUserException();
        }
        paymentGateway.payWith(paymentDetails);
    }
}
```