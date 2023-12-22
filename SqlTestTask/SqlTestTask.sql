-- 1. Вывести менеджеров у которых имеется номер телефона
SELECT * FROM Managers WHERE Phone IS NOT NULL;

-- 2. Вывести кол-во продаж за 20 июня 2021
SELECT COUNT(*) AS 'Кол-во продаж за 20 июня 2021' 
FROM Sells WHERE Date = '2021-06-20';

-- 3. Вывести среднюю сумму продажи с товаром 'Фанера'
SELECT AVG(Sum) AS 'Средняя сумма продажи с товаром Фанера' 
FROM Sells JOIN Products ON Sells.ID_Prod = Products.ID
WHERE Products.Name = 'Фанера';

-- 4. Вывести фамилии менеджеров и общую сумму продаж для каждого с товаром 'ОСБ'
SELECT Managers.Fio, SUM(Sells.Sum) AS 'Общая сумма продаж'
FROM Sells
JOIN Managers ON Sells.ID_Manag = Managers.ID
JOIN Products ON Sells.ID_Prod = Products.ID
WHERE Products.Name = 'ОСБ'
GROUP BY Managers.Fio;

-- 5. Вывести менеджера и товар, который продали 22 августа 2021
SELECT Managers.Fio, Products.Name AS 'Проданный товар'
FROM Sells
JOIN Managers ON Sells.ID_Manag = Managers.ID
JOIN Products ON Sells.ID_Prod = Products.ID
WHERE Date = '2021-08-22';

-- 6. Вывести все товары, у которых в названии имеется 'Фанера' и цена не ниже 1750
SELECT * FROM Products WHERE Name LIKE '%Фанера%' AND Cost >= 1750;

-- 7. Вывести историю продаж товаров, группируя по месяцу продажи и наименованию товара
SELECT MONTH(Date) AS 'Месяц', Products.Name AS 'Наименование товара', SUM(Count) AS 'Количество', SUM(Sum) AS 'Общая стоимость'
FROM Sells
JOIN Products ON Sells.ID_Prod = Products.ID
GROUP BY MONTH(Date), Products.Name;

-- 8. Вывести количество повторяющихся значений и сами значения из таблицы 'Товары', где количество повторений больше 1
-- Вариант запроса если нужно чтобы совпадало все кроме id
SELECT Name, Cost, Volume, COUNT(*) AS 'Количество повторений'
FROM Products
GROUP BY Name, Cost, Volume
HAVING COUNT(*) > 1;

-- Вариант запроса если нужно чтобы совпадали только названия товаров
SELECT Name, COUNT(*) AS 'Количество повторений'
FROM Products
GROUP BY Name
HAVING COUNT(*) > 1;