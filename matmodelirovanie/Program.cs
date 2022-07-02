using System;


namespace matmodelirovanie
{
    class program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Задача №3.\nНахождение оптимального распределения трех видов механизмов между тремя участками работ. ");//постановка задачи
                uint r1, r2;//размерности матрицы и векторов
                r1 = 3;
                r2 = 3;
                char otv;//переменная для диалога
                uint[,] arr = new uint[r2, r1];//матрица затрат
                uint[] m = new uint[r2];//вектор мощности
                uint[] n = new uint[r1];//вектор спроса
                uint valueM = 0, valueN = 0;//переменные для подсчета суммы
                uint min = uint.MaxValue;//переменная для поиска минимального элемента
                uint max = uint.MinValue;//переменная для поиска максимального элемента
                uint minL = uint.MinValue;
                uint maxx = uint.MinValue;//переменная для поиска максимального элемента
                int indI = 0, indJ = 0;//переменные для хранения индекса минимального элемента
                int indII = 0, indJJ = 0;//переменные для хранения индекса максимального элемента
                uint[,] raspr = new uint[r2, r1];//массив в котором хранятся значения, куда и сколько использовалось механизмов
                uint F = 0;//целевая функция
                string[] postavki = new string[0];//массив строк в котором прописано, кто с кем заключил договор
                string[,] itog = new string[r2, r1];//матрица, которая схожа с исходной, но в ней проставлены поставки через слеш
                Console.WriteLine();
                Console.WriteLine("Ввод данных об эффективности использования механизмов конкретного типа на участках работы:");
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        while (true) // ввод данных об эффективности использования механизмов конкретного типа на участках работы
                        {
                            try
                            {
                                Console.Write($"Введите производительность {i + 1}-го механизма при работе на {j + 1}-ом участке: ");
                                arr[i, j] = Convert.ToUInt32(Console.ReadLine());
                                if (arr[i, j] > 0)
                                    break;
                                else
                                {
                                    Console.WriteLine($"Не можеть быть 0");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Введены некорректные данные!");
                            }
                        }
                    }
                }
                while (true) // ввод данных об количестве механизмов каждого типа
                {
                    Console.WriteLine();
                    Console.WriteLine("Ввод данных о количестве механизмов каждого типа:");
                    for (int i = 0; i < m.Length; i++)
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write($"Введите количество механизмов {i + 1}-го типа: ");
                                m[i] = Convert.ToUInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Введены некорректные данные!");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Ввод данных о запрашиваемых механизмах для каждого участка:");
                    for (int i = 0; i < n.Length; i++)
                    {

                        while (true) //ввод данных о запрашиваемых механизмах для каждого участка
                        {
                            try
                            {
                                Console.Write($"Введите количество запрашиваемых механизмов для {i + 1}-го участка работы: ");
                                n[i] = Convert.ToUInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Введены некорректные данные!");
                            }
                        }
                    }
                    for (int i = 0; i < m.Length; i++) // проверка на одинаковость суммы векторов
                    {
                        valueM += m[i];
                    }
                    for (int i = 0; i < n.Length; i++)
                    {
                        valueN += n[i];
                    }
                    if (valueM == valueN)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вектор механизмов и участков работ должны быть равны, повторите ввод!");
                        valueM = 0;
                        valueN = 0;
                    }
                }
                Console.WriteLine();
                //вывод таблицы производительности и векторов
                Console.WriteLine("\nТаблица производительности:");
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write($"{arr[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Вектор механизмов (m):");
                for (int i = 0; i < m.Length; i++)
                {
                    Console.Write($"{m[i]} ");
                }
                Console.WriteLine();
                Console.WriteLine("\nВектор участков работы (n):");
                for (int i = 0; i < n.Length; i++)
                {
                    Console.Write($"{n[i]} ");
                }
                //Возможность изменять матрицу
                while (true)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("\nХотите изменить данные в таблице?\nЕсли нет, то нажмите на кнопку Д на клавиатуре\nЕсли да, то можете нажать на любую другую кнопку\nОтвет: ");
                            otv = Convert.ToChar(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Введены некорректные данные!");
                        }
                    }
                    if (otv.Equals('l') || otv.Equals('д') || otv.Equals('Д') || otv.Equals('L'))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Введите номер вида механизма, затем номер участка работ, который вы хотите изменить\nОтвет:\n");
                        while (true)
                        {
                            int i = Convert.ToInt32(Console.ReadLine()), j = Convert.ToInt32(Console.ReadLine());
                            if (i < r2 + 1 && j < r1 + 1)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Введите новое значение: ");
                                        uint temp = Convert.ToUInt32(Console.ReadLine());
                                        if (temp != 0)
                                        {
                                            Console.WriteLine($"Вы изменили {i} {j} ячейку таблицы c {arr[i - 1, j - 1]} на {temp}");
                                            arr[i - 1, j - 1] = temp;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Нельзя ввести нулевое значение! Повторите ввод");
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Введены некорректные данные!");
                                    }
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введенная размерность не соответствует! Повторите ввод");
                            }
                        }
                    }
                }
                Console.Clear();
                Console.WriteLine("\nМатрица:"); //вывод матрицы тарифов
                Console.Write(" ");
                for (int i = 0; i < n.Length; i++)
                {
                    Console.Write($"{n[i]} ");
                }
                Console.WriteLine();
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    Console.Write($"{m[i]} ");
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write($"{arr[i, j]} ");
                    }
                    Console.WriteLine();
                }
                for (int i = 0; i < raspr.GetLength(0); i++) // создание таблицы распределения и заполнение её пока что нулями
                {
                    for (int j = 0; j < raspr.GetLength(1); j++)
                    {
                        raspr[i, j] = 0;
                    }
                }
                max = uint.MinValue; // Максимальное значение
                for (int i = 0; i < arr.GetLength(0); i++) // поиск максимального и преобразование по формуле Maxl+1-A
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] > max)
                        {
                            max = arr[i, j];
                        }
                    }
                }
                max = max + 1;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        arr[i, j] = max - arr[i, j]; // преобразование по формуле Maxl+1-A
                    }
                }
                Console.WriteLine("\nПреобразование значений матрицы:"); // вывод преобразованной таблицы
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write($" {arr[i, j]} ");
                    }
                    Console.WriteLine();
                }
                while (true) //алгоритм решения по минимальному элементу
                {
                    min = uint.MaxValue;
                    valueM = 0;
                    valueN = 0;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (arr[i, j] < min && raspr[i, j] == 0 && m[i] != 0 && n[j] != 0)
                            {
                                min = arr[i, j];
                                indI = i;
                                indJ = j;
                            }
                        }
                    }
                    if (n[indJ] != 0 && m[indI] != 0)
                    {
                        if (n[indJ] > m[indI])
                        {
                            raspr[indI, indJ] = m[indI];
                            n[indJ] -= m[indI];
                            m[indI] = 0;
                        }
                        else
                        {
                            raspr[indI, indJ] = n[indJ];
                            m[indI] -= n[indJ];
                            n[indJ] = 0;
                        }
                    }
                    for (int i = 0; i < m.Length; i++) // проверка на то, что первоначальное рапределение выполнено полностью
                    {
                        valueM += m[i];
                    }
                    for (int i = 0; i < n.Length; i++)
                    {
                        valueN += n[i];
                    }
                    if (valueM == 0 && valueN == 0)
                    {
                        break;
                    }
                }
                Console.WriteLine("\nПервоначальное распределение методом минимального элемента:");
                uint zap = 0;//переменная для подсчета заполненных клеток, понадобиться для проверки вырожденности
                for (int i = 0; i < itog.GetLength(0); i++) //вывод матрицы, в которой через слеш написаны поставки
                {
                    for (int j = 0; j < itog.GetLength(1); j++)
                    {
                        if (raspr[i, j] != 0)
                        {
                            itog[i, j] = $"{arr[i, j]}/{raspr[i, j]}";
                            zap = zap + 1;
                        }
                        else
                        {
                            itog[i, j] = $"{arr[i, j]} ";
                        }
                        Console.Write($"{itog[i, j]} ");
                    }
                    Console.WriteLine();
                }
                uint strst = r1 + r2 - 1;
                minL = uint.MaxValue; //проверка на вырожденность
                Console.WriteLine();
                Console.WriteLine("\nПроверка на вырожденность:");
                if (strst != zap) //если количество заполненных клеток не равно столбцы+строки-1, то приписание 0 в мин поставку из свободных
                {
                    Console.WriteLine("Таблица вырождена");
                    for (int i =
                    0; i < arr.GetLength(0); i++) // поиск миним. эл среди не заполненных
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (raspr[i, j] == 0)
                            {
                                if (arr[i, j] < minL)
                                {
                                    minL = arr[i, j];
                                    indI = i;
                                    indJ = j;
                                }
                            }
                        }
                    }
                    raspr[indI, indJ] = 9999; // помечаем не заполненные минимальные клетки
                    for (int i = 0; i < itog.GetLength(0); i++)
                    {
                        for (int j = 0; j < itog.GetLength(1); j++)
                        {
                            if (raspr[i, j] != 0 && raspr[i, j] != 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j]}";

                            }
                            else if (raspr[i, j] == 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j] - 9999}"; //добавление поставки 0
                            }
                            else
                            {
                                itog[i, j] = $"{arr[i, j]} ";
                            }
                            Console.Write($"{itog[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Таблица не вырождена");
                }
            metka:

                Console.WriteLine();
                int[] U = new int[r2]; // начальное заполнение массива потенциалов, для дальнейшего решения
                int[] V = new int[r1];
                for (int i = 0; i < r1; i++)
                {
                    U[i] = 99;
                    V[i] = 99;
                }
                //Для заполненных клеток рассчитываются потенциалы Uj и Vi
                // проверка на то, является ли ячейка пустой и не записаны ли для неё потенциалы, если нет, то высчитывается потенциал
                Console.WriteLine("Расчёт потенциалов");
                while (true)
                {
                    bool proverk = true; // для выхода из бесконечного цикла
                    U[0] = 0; // первому элементу u всегда присваивается 0
                    for (int j = 0; j < r2; j++)
                    {
                        for (int i = 0; i < r1; i++)
                        {
                            if (raspr[i, j] != 0)
                            {
                                if (U[j] != 99)
                                {
                                    V[i] = Convert.ToInt32(arr[i, j] - U[j]); // растановка потенциалов V
                                }
                                else if (V[i] != 99)
                                {
                                    U[j] = Convert.ToInt32(arr[i, j] - V[i]);// растановка потенциалов U
                                }
                            }
                        }
                    }
                    for (int i = 0; i < r1; i++) // если все потенциалы расставлены, т.е они перестали быть = 99, то выходим из бессконечного цикла
                    {
                        if (U[i] == 99 || V[i] == 99)
                        {
                            proverk = false;
                        }
                    }
                    if (proverk)
                    {
                        break;
                    }
                }
                Console.WriteLine("Таблица с раставленными потенциалами:"); // вывод таблицы с потенциалами
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write($"{itog[i, j]} ");
                    }
                    Console.Write($" {V[i]}");
                    Console.WriteLine();
                }
                for (int i = 0; i < arr.GetLength(1); i++)
                {
                    Console.Write($"{U[i]} ");
                }
                Console.WriteLine();
                Console.WriteLine();
                int[,] Delta = new int[r2, r1]; // задаеем массив дельт
                int maxxx = 0; // задаем максимпальной дельте 0
                Console.WriteLine("Для пустых клеток расчет дельты:");
                for (int i = 0; i < arr.GetLength(0); i++) //определение оптимальности - расчет дельта
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (raspr[i, j] == 0) // если клетка пустая, то считаем дельту
                        {
                            Delta[i, j] = Convert.ToInt32(U[j] + V[i] - arr[i, j]);
                            if (Delta[i, j] > maxxx) // если посчитанная дельта больше макс.дельты, то присваиваем максимальной дельте полученную дельту и запоминаем её индексы
                            {
                                maxxx = Delta[i, j];
                                indI = i;
                                indJ = j;
                            }
                            if (Delta[i, j] == maxxx && Delta[i, j] != 0) // выводим положительные дельты
                            {
                                Console.WriteLine($"{i + 1}{j + 1} = {Delta[i, j]} - max");
                            }
                            else // выводим не положительные дельты
                            {
                                Console.WriteLine($"{i + 1}{j + 1} = {Delta[i, j]}");
                            }
                        }
                    }
                }
                Console.WriteLine();
                int[,] optimal = new int[r2, r1]; // массив оптимальности, для построения цикла перераспределения
                int indi = indI; // переприсвоение индексов максимальной дельты
                int indj = indJ;
                bool proverk1 = true; // для выхода из бесконечного цикла
                bool proverk2 = true;
                if (maxxx > 0) // проверка на оптимальность
                {
                    Console.WriteLine("Решение не оптимальное:");
                    for (int i = 0; i < optimal.GetLength(0); i++)
                    {
                        for (int j = 0; j < optimal.GetLength(0); j++)
                        {
                            optimal[i, j] = 0; // заполнение массива 0, понадобится для дальнейших расчетов
                            if (i == indI && j == indJ) //если элемент является максимальрной дельтой, то присваиваем ему 1 (отличный от всех)
                            {
                                optimal[i, j] = 1;
                            }
                        }
                    }

                    F = 0; // промежуточный вывод целувой функции
                    for (int i = 0; i < raspr.GetLength(0); i++)
                    {
                        for (int
                        j = 0; j < raspr.GetLength(1); j++)
                        {
                            if (raspr[i, j] != 0 && raspr[i, j] != 9999)
                            {
                                F += raspr[i, j] * arr[i, j];
                            }
                        }
                    }
                    Console.WriteLine($"\nF = {F} у.д.е");
                    Console.WriteLine();
                    while (true)
                    {
                        proverk2 = true; //для бесконечного цикла
                        for (int i = 0; i < optimal.GetLength(0); i++)
                        {
                            for (int j = 0; j < optimal.GetLength(1); j++)
                            {
                                if (i != indi && j != indj && raspr[i, j] != 0 && optimal[i, j] != 2) // находим заполненные клетки, которые не являются максимальной дельтой
                                {
                                    optimal[i, j] = 2; // присваиваем им 2, для отличия
                                    indI = i;
                                    indJ = j;
                                    proverk2 = false; // выход из бесконечного цикла по j
                                    break;
                                }
                            }
                            if (!proverk2) // выход из бесконечного цикла по i
                            {
                                break;
                            }
                        }
                        if (raspr[indI, indj] != 0 && raspr[indi, indJ] != 0) // строим цикл перераспр.
                        {
                            optimal[indI, indj] = optimal[indi, indJ] = -1; //помечаем клетку от куда будем вычитать -x
                            optimal[indI, indJ] = 1;//помечаем клетку куда будем прибавлять +x
                            proverk1 = false;
                        }
                        if (!proverk1) // выход из бесконечного цикла
                        {
                            break;
                        }
                    }
                    uint minT = uint.MaxValue;
                    for (int i = 0; i < optimal.GetLength(0); i++) // выбираем X=MIN(-x)
                    {
                        for (int j = 0; j < optimal.GetLength(1); j++)
                        {
                            if (optimal[i, j] == -1)
                            {
                                if (raspr[i, j] < minT)
                                {
                                    minT = raspr[i, j];
                                }
                            }
                        }
                    }
                    Console.WriteLine($"X = MIN = {minT}");
                    Console.WriteLine();
                    for (int i = 0; i < optimal.GetLength(0); i++) // выполняем перераспределение
                    {
                        for (int j = 0; j < optimal.GetLength(1); j++)
                        {
                            if (optimal[i, j] == -1) // если клетка помечена -1, из её поставки вычитаем MIN
                            {
                                raspr[i, j] -= Convert.ToUInt32(minT);
                            }
                            if (optimal[i, j] == 1 && raspr[i, j] != 9999) // если клетка помечена 1 и является заполненной к ней прибавляем MIN
                            {
                                raspr[i, j] += Convert.ToUInt32(minT);
                            }
                            else if (optimal[i, j] == 1 && raspr[i, j] == 999) //если клетка помечена 1 и не заполнена к ней прибавляем MIN
                            {
                                raspr[i, j] += Convert.ToUInt32(minT) - 999;
                            }
                        }
                    }
                    zap = 0; // вывод матрицы после цикла перераспределения
                    Console.WriteLine("Построенные цикл перераспределения");
                    for (int i = 0; i < itog.GetLength(0); i++)
                    {
                        for (int j = 0; j < itog.GetLength(1); j++)
                        {
                            if (raspr[i, j] != 0 && raspr[i, j] != 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j]} ";
                                zap++;
                            }
                            else if (raspr[i, j] == 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j] - 9999} ";
                                zap++;
                            }
                            else
                            {
                                itog[i, j] = $"{arr[i, j]} ";
                            }
                            Console.Write($"{itog[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    goto metka; // опять проверка на оптимальность
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Решение оптимальное:");
                    max = uint.MinValue; // Максимальное значение
                    for (int i = 0; i < arr.GetLength(0); i++) // поиск максимального и преобразование по формуле Maxl+1-A
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (arr[i, j] > max)
                            {
                                max = arr[i, j];
                            }
                        }
                    }
                    max = max + 1;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            arr[i, j] = max - arr[i, j]; // преобразование по формуле Maxl+1-A
                        }
                    }
                    Console.WriteLine("\nОбратное преобразование матрицы:"); // вывод преобразованной таблицы
                    for (int i = 0; i < itog.GetLength(0); i++)
                    {
                        for (int j = 0; j < itog.GetLength(1); j++)
                        {
                            if (raspr[i, j] != 0 && raspr[i, j] != 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j]}";
                                zap++;
                            }
                            else if (raspr[i, j] == 9999)
                            {
                                itog[i, j] = $"{arr[i, j]}/{raspr[i, j] - 9999}";
                                zap++;
                            }
                            else
                            {
                                itog[i, j] = $"{arr[i, j]} ";
                            }
                            Console.Write($"{itog[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    F = 0;//подсчет и вывод целевой функции
                    for (int i = 0; i < raspr.GetLength(0); i++)
                    {
                        for (int j = 0; j < raspr.GetLength(1); j++)
                        {
                            if (raspr[i, j] != 0 && raspr[i, j] != 9999)
                            {
                                F += raspr[i, j] * arr[i, j];
                            }
                        }
                    }
                    Console.WriteLine($"\nF = {F} у.д.е");
                }
                // повтор выполнения программы
                while (true)
                {
                    try
                    {
                        Console.Write("\nХотите повторить выполнение программы?\nЕсли да, то нажмите на кнопку Y(на англ) на клавиатуре\nЕсли нет, то можете нажать на любую другую кнопку\nОтвет: ");
                        otv = Convert.ToChar(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                if (!(otv.Equals('Y') || otv.Equals('y') || otv.Equals('н') || otv.Equals('Н')))
                {
                    Console.WriteLine("Программу выполнила студентка группы 31П\nЛебедева Александра Федоровна");
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }
        }
    }
}
