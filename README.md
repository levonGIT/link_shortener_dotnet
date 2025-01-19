# Api сокращатель ссылок с использованием технологии .net
### В качестве интерфейса для взаимодействия с API использовался Swagger
1.	Пользователь может зарегистрироваться и авторизоваться в сервисе. 
2.	Пользователь может вставить ссылку и получить сокращенный вариант (для ссылки генерируется короткий уникальный код). 
3.	Пользователь может увидеть список сокращенных ссылок, как всех так и только своих (если авторизован), сделав соответствующий запрос. 
4.	Пользователь может перейти по короткой ссылке и его перенаправит на оригинальный адрес.
5.  При переходе по ссылке увеличивается счетчик переходов по этой ссылке.


База данных использована MSSQL.  
Для авторизации используется JWT token.


# Api url shortener using .net
### Used Swagger to interact with the api
1.	User is able to register and login. 
2.	User is able to generate short url by passing the original url (short unique code will be generated for each url). 
3.	User is able to get list of short urls. 
4.	User is able to follow short url and will be redirected to original url.
5.  Each short url click increases click counter of the link.


MSSQL is used as a primary DB.  
JWT is used for authentication.
