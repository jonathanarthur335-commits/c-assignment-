# Student Results Processing System

A C# console application that collects results for three students, validates five course scores, calculates totals and averages, assigns grades, and displays pass/fail status.

## Features

- Menu-based console interface
- Required student detail validation
- Score validation from 0 to 100
- Reports for all three students
- Total, average, grade, and academic status calculations
- Best student, lowest average, and class average summary
- Methods and a `Student` class for clear code organization

## Run the project

```powershell
dotnet run --project .\StudentResultsProcessingSystem.csproj
```

## Grading rules

| Average | Grade |
|---:|:---:|
| 80-100 | A |
| 70-79.99 | B |
| 60-69.99 | C |
| 50-59.99 | D |
| Below 50 | F |

An average of 50 or above is **Passed**; below 50 is **Failed**.
