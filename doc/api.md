В качестве апи используется RPC API с одной точкой входа:
http://opencorpora.org/v1.0

### Request
POST запрос с двумя полями:

    action = 'название_метода'  - обязательный
    data = json object          - опциональный
    token = 'token полученный через метод login' - опциональный

пример:

    action = "get_user_stat"
    data = {"id":1234, "full":1}

### Response
json object, у которого будет только поле "error" с текстом ошибки
или только поле "result" содержащее json object

### Client
Необходимые проверки в приложении:

1. проверка доступа в интернет + пинг сервера
2. обработка ошибочных HTTP ответов (4хх/5хх статусы)
3. обработка "error"/валидности json в "result"

Аутентификация работает на основе токена юзера, получаемого через
вызов экшена "login"

### actions

    параметры:
    + обязательные
    * опциональные

    "welcome"

    "search"
    + query
    * all_forms

    "login"
    + login password

    "register"
    + 'login passwd passwd_re email

    "all_stat"

    "get_available_morph_tasks"
    + user_id

    "get_morph_task"
    + user_id pool_id size

    "save_morph_task"
    + user_id answers
    example.: {"user_id" : "3678", "answers": [ {"id": "2449933", "answer":"1"}, {"id": "2450187", "answer":"1"} ]}


    "get_user"
    + user_id

    "save_user"
    * shown_name OR email (+ passwd user_id) OR passwd (+old_passwd user_id)

    "user_stat"
    + user_id

    "grab_badges
    + user_id
