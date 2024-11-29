# BSDigitalInterview

Navodila za zagon API-ja preko dockerja:

1. Odpremo CMD in se premaknemo se v mapo CryptoExchangeApi
2. Poženemo ukaze:

`docker build -t cryptoexchangeapi -f Dockerfile .`

`docker run --name cryptoexchangeapi -it --rm -p 8080:8080 cryptoexchangeapi 
`

API lahko testiramo z uporabo:
_CryptoExchangeApi.http_

Primer: http://localhost:8080/CryptoExchange/orders/executionPlan?OrderType=buy&Amount=10

Endpoint sprejme 2 query parametra:

OrderType => buy, sell

Amount => Numeric [0...]