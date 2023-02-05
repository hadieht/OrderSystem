# OrderSystem

You can use this json request body format for creating new order :

```json
{
  "customerName": "John doe",
  "email": "John@mail.com",
  "address": {
    "postalCode": "1111AA",
    "houseNumber": 1
  },
  "items": [
    {
      "productType": "PhotoBook",
      "quantity": 1
    },
	{
      "productType": "Mug",
      "quantity": 2
    },
	{
      "productType": "Calendar",
      "quantity": 2
    }
  ]
}
```
