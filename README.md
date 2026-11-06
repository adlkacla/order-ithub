# OrderHub

Учебный проект по разделу «Коллекции, LINQ, перечислители и итераторы» (КТ31–39).
Консольное приложение на .NET 9.

## Сборка и запуск

```bash
dotnet build OrderHub.sln
dotnet run --project src/OrderHub
```

## Реализовано по этапам

1. **List, Queue, Stack** — `Services/OrderService.cs`: создание заказов, очередь на обработку, стек отмены, отчёты по коллекциям.
2. **LinkedList** — `Services/OrderService.cs`: история заказов, вставка после текущего (`AddAfter`), поиск заказа по клиенту.
3. **Dictionary** — `Services/WarehouseService.cs`: приход и расход товара, проверка остатка через `TryGetValue`, снятие товара с учёта через `Remove`.
4. **Сортировка** — `Domain/Product.cs`, `Services/CatalogService.cs`: `IComparable<Product>`, `IComparer<Product>`, сортировка лямбдой `Comparison<Product>`.
5. **LINQ** — `Services/ReportService.cs`: `Where`, `OrderBy`, `Select`, `Count`, `Any`, `All`, `First`.
6. **Группировка** — `Services/ReportService.cs`: `GroupBy`, сводка по категориям, средняя цена, самая дорогая категория.
7. **Соединение и топ-N** — `Services/ReportService.cs`: `Join` заказов с клиентами, `Take(3)`, выручка по годам, самый активный клиент.
8. **Итераторы** — `Services/OrderStream.cs`: `yield return`, ленивые последовательности, `Take` против `ToList`.
9. **Свой итератор** — `Collections/CircularBuffer.cs`: кольцевой буфер, `foreach`, `Reverse`, `Count`, `IsFull`.

## Структура

```
src/OrderHub/
├── Collections/   CircularBuffer.cs
├── Domain/        Order.cs, Product.cs, Customer.cs
├── Services/      OrderService.cs, WarehouseService.cs, CatalogService.cs, ReportService.cs, OrderStream.cs
└── Program.cs     демонстрация всех этапов
docs/report.md     отчёт по проекту
```

Комментарии «ЗАДАЧА» в коде сохранены как пояснения к реализации.
