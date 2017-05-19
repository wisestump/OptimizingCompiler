# Название задачи: /*название задачи*/

### Выполнено командой: /*имя команды*/

### Постановка задачи: /*развернуто, 2-3 абзаца*/

### От каких проектов зависит:

  - 1 проект
  - 2 проект
  - 3 проект

### Зависимые проекты:

  - 1 проект
  - 2 проект
  - 3 проект

### Теория

> В этот блок поместить теорию по вашей задаче.  Объем - более 3 абзацев.
Должно быть больше, чем постановка задачи.
Материал брать из лекций и доп.источников.
Графики и изображения - по необходимости. Не обязательно!

### Входные данные:
 - Что дано для задачи.

### Выходные данные:
 - Что нужно найти.

### Используемые структуры данных

 -
 -

### Реализация алгоритма

/*вставка основного кода с комментариями*/
```
Trace.WriteLine("");
    var findNL = SearchNaturalLoops.FindAllNaturalLoops(g);
    foreach (var nl in findNL)
    {
        Trace.Write(nl.Key.Source.BlockId + "->" + nl.Key.Target.BlockId + ": ");
        foreach (var node in nl.Value)
            Trace.Write(node.ToString() + " ");
            Trace.WriteLine("");
    }
```

### Пример использования

/*результат работы*/

### Тест

/*небольшой тест на входных данных.*/