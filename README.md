# WordCount

Консольное приложение для подсчёта количества вхождений заданного слова в текстовом файле. Реализовано четыре алгоритма поиска. В качестве тестового текста используется роман Льва Толстого «Война и мир».

## Структура решения

```
wordCount/
├── wordCount/          # Основной проект (алгоритмы + бенчмарки)
│   ├── WordCount.cs    # Реализация алгоритмов
│   └── BenchmarkFinding.cs
├── TestProject/        # Юнит-тесты (NUnit)
│   └── UnitTest1.cs
├── text.txt            # Тестовый текст («Война и мир»)
└── wordCount.slnx
```

## Алгоритмы

Все методы принимают `string[] text` и `string word`, возвращают `long`.

| Метод | Описание |
|---|---|
| `WordCount_IndexOf` | Посимвольный поиск через `String.IndexOf` с проверкой границ слова |
| `WordCount_Linq` | Разбивка на слова через `Split` + фильтрация через LINQ |
| `WordCount_Binary` | Разбивка на слова, сортировка массива, бинарный поиск + обход соседей |
| `WordCount_Regex` | Поиск через регулярное выражение с lookahead/lookbehind для кириллицы |

Все методы регистронезависимы (`OrdinalIgnoreCase`).

Разделители слов:

```
пробел, табуляция, перевод строки, . , ! ? ; : - ( ) [ ] { } « » — – / \ " ' ` _ * # @ & % + = < > | ^ ~
```

## Запуск

```bash
# Сборка и запуск основного приложения
dotnet run --project wordCount

# Запуск тестов
dotnet test

# Запуск бенчмарков (только Release)
dotnet run --project wordCount -c Release
```

## Тесты

Проект `TestProject`, фреймворк — NUnit.

Тесты проверяют слова **он**, **она**, **они** на полном тексте «Войны и мира»:

- `Test_He` — все методы возвращают одинаковое количество вхождений слова «он»
- `Test_She` — аналогично для «она»
- `Test_They` — аналогично для «они»

Эталоном служит результат `WordCount_IndexOf` — остальные методы сравниваются с ним через `Assert.Multiple`.

```bash
dotnet test
```

## Бенчмарки

Проект использует [BenchmarkDotNet](https://benchmarkdotnet.org/). Класс `BenchmarkFinding` замеряет время выполнения и потребление памяти каждого алгоритма.

Параметр `Word` задаёт искомое слово. Базовый метод (Baseline) — `IndexOf`.

Запуск:

```bash
dotnet run --project wordCount -c Release
```

Пример ожидаемых результатов (порядок величин):

| Method  | Word | Mean      | Ratio | Allocated |
|---------|------|-----------|-------|-----------|
| IndexOf | он   | ~5 ms     | 1.00  | ~1 KB     |
| Linq    | он   | ~15 ms    | ~3x   | ~5 MB     |
| Binary  | он   | ~25 ms    | ~5x   | ~10 MB    |
| Regex   | он   | ~30 ms    | ~6x   | ~3 MB     |

> Точные числа зависят от машины и размера файла.

## Зависимости

| Пакет | Версия | Проект |
|---|---|---|
| BenchmarkDotNet | 0.14.0 | wordCount |
| NUnit | 4.3.2 | TestProject |
| NUnit3TestAdapter | 5.0.0 | TestProject |
| Microsoft.NET.Test.Sdk | 17.14.0 | TestProject |
| coverlet.collector | 6.0.4 | TestProject |

## Требования

- .NET 10.0
- Файл `text.txt` в корне решения
