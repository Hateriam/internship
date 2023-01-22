using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskExam
{
    internal class TaskSolver
    {
        public static void Main(string[] args)
        {
            TestGenerateWordsFromWord();
            TestMaxLengthTwoChar();
            TestGetPreviousMaxDigital();
            TestSearchQueenOrHorse();
            TestCalculateMaxCoins();

            Console.WriteLine("All Test completed!");
        }

        /// задание 1) Слова из слова
        public static List<string> GenerateWordsFromWord(string word, List<string> wordDictionary)
        {
            List<string> correctWords = new List<string>();
            string newWord = "";
            bool checkWordInList = correctWords.Contains(newWord);
            Dictionary<char, int> letters = LettersInWord(word);

            foreach (string element in wordDictionary)
            {
                for (int i = 0; i < element.Length; i++)
                {
                    if (letters.ContainsKey(element[i]) && letters[element[i]] > 0)
                    {
                        letters[element[i]] -= 1;
                        newWord += element[i];
                    }
                    else
                    {
                        newWord = "";
                        letters.Clear();
                        letters = LettersInWord(word);
                        break;
                    }
                }
                if (!checkWordInList && element == newWord)
                {
                    correctWords.Add(element);
                    newWord = "";
                    letters.Clear();
                    letters = LettersInWord(word);
                }
            }
            correctWords.Sort();

            return correctWords;
        }

        public static Dictionary<char, int> LettersInWord(string word)
        {
            Dictionary<char, int> checkLetters = new Dictionary<char, int>();

            for (int i = 0; i < word.Length; i++)
            {
                if (!checkLetters.ContainsKey(word[i]))
                {
                    checkLetters.Add(word[i], 1);
                }
                else
                {
                    checkLetters[word[i]] += 1;
                }
            }
            return checkLetters;
        }

        /// задание 2) Два уникальных символа
        public static int MaxLengthTwoChar(string stringSymbol)
        {
            int maxLength = 0;
            List<int> variantsLength = new List<int>();
            DeleteChar(stringSymbol, variantsLength);
            foreach (var num in variantsLength)
            {
                if (num > maxLength)
                {
                    maxLength = num;
                }
            }
            return maxLength;
        }

        public static bool DeleteChar(string stringSymbol, List<int> list)
        {
            string newString = "";
            List<char> chars = new List<char>();
            for (int i = 0; i < stringSymbol.Length; i++)
            {
                if (stringSymbol.Length > 2 && CheckMoreUniqueTwoSymbol(newString) && !chars.Contains(stringSymbol[i]))
                {
                    chars.Add(stringSymbol[i]);
                    int deletedSymbol = i;
                    int j = 0;

                    while (stringSymbol.Length > 2 && j < stringSymbol.Length)
                    {
                        if (stringSymbol[j] != stringSymbol[deletedSymbol])
                        {
                            newString += stringSymbol[j];
                            j++;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (CheckMoreUniqueTwoSymbol(newString))
                    {
                        DeleteChar(newString, list);
                        newString = "";
                    }
                    else
                    {
                        if (!CheckDoubleChar(newString))
                        {
                            list.Add(newString.Length);
                            newString = "";
                        }
                        else
                        {
                            newString = "";
                        }
                    }
                }
                else if (stringSymbol.Length == 2 && !CheckMoreUniqueTwoSymbol(stringSymbol))
                {
                    list.Add(stringSymbol.Length);
                }
                else
                {
                    continue;
                }
            }
            return false;
        }
        public static bool CheckMoreUniqueTwoSymbol(string chars)
        {
            bool isMoreThanTwo = false;
            List<char> differentSymbols = new List<char>();

            for (int i = 0; i < chars.Length; i++)
            {
                if (!differentSymbols.Contains(chars[i]))
                {
                    differentSymbols.Add(chars[i]);

                }
            }
            if (differentSymbols.Count != 2)
            {
                return isMoreThanTwo = true;
            }
            return isMoreThanTwo;
        }
        public static bool CheckDoubleChar(string chars)
        {
            bool isDouble = false;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                if (chars[i] == chars[i + 1])
                {
                    isDouble = true;
                    break;
                }
            }
            return isDouble;
        }

        /// задание 3) Предыдущее число
        public static long GetPreviousMaxDigital(long value)
        {
            long answer = -1;
            char[] numbersInNumber = NumbersInNumber(value);
            int lengthNumber = numbersInNumber.Length;
            List<string> results = new List<string>();
            Permutation(numbersInNumber, lengthNumber, results);

            foreach (string num in results)
            {
                long check = Convert.ToInt64(num);
                string check1 = Convert.ToString(check);
                int lengthCheckedNumber = check1.Length;
                
                if (check > answer && check < value && lengthNumber == lengthCheckedNumber)
                {
                    answer = check;
                }
            }
            return answer;
        }
        public static char[] NumbersInNumber(long number)
        {
            string temp = number.ToString();
            char[] numbersInNumber = new char[temp.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                numbersInNumber[i] = temp[i];
            }
            return numbersInNumber;
        }

        public static List<string> Permutation(char[] array, int length, List<string> list)
        {
            string result = "";

            if (length == 1)
            {
                foreach (char c in array)
                {
                    result += c;
                }
                list.Add(result);

                return list;
            }
            for (int i = 0; i < length; i++)
            {
                Permutation(array, length - 1, list);
                if (length % 2 == 0)
                {
                    char temp = array[i];
                    array[i] = array[length - 1];
                    array[length - 1] = temp;
                }
                else
                {
                    char temp = array[0];
                    array[0] = array[length - 1];
                    array[length - 1] = temp;
                }
            }
            return list;
        }

        /// задание 4) Конь и Королева
        public static List<int> SearchQueenOrHorse(char[][] map)
        {
            return new List<int> { HorseMove(map), QueenMove(map) };
        }

        public static List<int> WhereStart(char[][] map)
        {
            List<int> start = new List<int>();

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 's')
                    {
                        start.AddRange(new int[] { i, j });
                        return start;
                    }
                }
            }
            return start;
        }

        public static int HorseMove(char[][] map)
        {
            List<int> start = WhereStart(map);
            int bestResult = -1;
            int steps = 0;
            int x = start[0];
            int y = start[1];
            List<int> resultsHorse = new List<int>();
            CheckHorseMove(map, x, y, steps, resultsHorse);

            foreach (var num in resultsHorse)
            {
                if (bestResult == -1 && num != 0)
                {
                    bestResult = num;
                }
                else if (bestResult != 0 && num < bestResult)
                {
                    bestResult = num;
                }
                else if (bestResult == num || bestResult < num)
                {
                    continue;
                }
                else
                {
                    bestResult = -1;
                }
            }
            return bestResult;
        }
        public static int QueenMove(char[][] map)
        {
            List<int> start = WhereStart(map);
            int bestResult = -1;
            int steps = 0;
            int x = start[0];
            int y = start[1];
            List<int> resultsQueen = new List<int>();
            CheckQueenMove(map, x, y, steps, resultsQueen);

            foreach (var num in resultsQueen)
            {
                if (bestResult == -1 && num != 0)
                {
                    bestResult = num;
                }
                else if (bestResult != 0 && num < bestResult)
                {
                    bestResult = num;
                }
                else if (bestResult == num || bestResult < num)
                {
                    continue;
                }
                else
                {
                    bestResult = -1;
                }
            }
            return bestResult;
        }
        public static bool CheckHorseMove(char[][] test, int x, int y, int steps, List<int> resultsHorse)
        {
            if (test[x][y] != 'e' && x - 2 >= 0 && y + 1 < test[x].Length && test[x - 2][y + 1] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x -= 2;
                y += 1;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x += 2;
                    y -= 1;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x - 1 >= 0 && y + 2 < test[x].Length && test[x - 1][y + 2] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x -= 1;
                y += 2;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x += 1;
                    y -= 2;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x + 1 < test.Length && y + 2 < test[x].Length && test[x + 1][y + 2] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x += 1;
                y += 2;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x -= 1;
                    y -= 2;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x + 2 < test.Length && y + 1 < test[x].Length && test[x + 2][y + 1] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x += 2;
                y += 1;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x -= 2;
                    y -= 1;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x + 2 < test.Length && y - 1 >= 0 && test[x + 2][y - 1] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x += 2;
                y -= 1;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x -= 2;
                    y += 1;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x + 1 < test.Length && y - 2 >= 0 && test[x + 1][y - 2] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x += 1;
                y -= 2;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x -= 1;
                    y += 2;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x - 1 >= 0 && y - 2 >= 0 && test[x - 1][y - 2] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x -= 1;
                y -= 2;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x += 1;
                    y += 2;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] != 'e' && x - 2 >= 0 && y - 1 >= 0 && test[x - 2][y - 1] != 'x')
            {
                char temp = test[x][y];
                test[x][y] = 'x';
                x -= 2;
                y -= 1;
                steps++;
                if (CheckHorseMove(test, x, y, steps, resultsHorse) == false)
                {
                    steps--;
                    x += 2;
                    y += 1;
                    test[x][y] = temp;
                }
            }
            if (test[x][y] == 'e')
            {
                resultsHorse.Add(steps);
                return false;
            }
            return false;
        }
        public static bool CheckQueenMove(char[][] test, int x, int y, int steps, List<int> resultsQueen)
        {
            if (test[x][y] != 'e' && x - 1 >= 0 && test[x - 1][y] != 'x')
            {
                int counter = 0;
                while (x > 0 && test[x - 1][y] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x--;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x++;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && x - 1 >= 0 && y + 1 < test[x].Length && test[x - 1][y + 1] != 'x')
            {
                int counter = 0;
                while (x > 0 && y < test[x].Length - 1 && test[x - 1][y + 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x--;
                    y++;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x++;
                        y--;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && y + 1 < test[x].Length && test[x][y + 1] != 'x')
            {
                int counter = 0;
                while (y < test[x].Length - 1 && test[x][y + 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    y++;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        y--;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && x + 1 < test.Length && y + 1 < test[x].Length && test[x + 1][y + 1] != 'x')
            {
                int counter = 0;
                while (x < test.Length - 1 && y < test[x].Length - 1 && test[x + 1][y + 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x++;
                    y++;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x--;
                        y--;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && x + 1 < test.Length && test[x + 1][y] != 'x')
            {
                int counter = 0;
                while (x < test.Length - 1 && test[x + 1][y] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x++;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x--;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && x + 1 < test.Length && y - 1 >= 0 && test[x + 1][y - 1] != 'x')
            {
                int counter = 0;
                while (x < test.Length - 1 && y > 0 && test[x + 1][y - 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x++;
                    y--;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x--;
                        y++;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && y - 1 >= 0 && test[x][y - 1] != 'x')
            {
                int counter = 0;
                while (y > 0 && test[x][y - 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    y--;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        y++;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] != 'e' && x - 1 >= 0 && y - 1 >= 0 && test[x - 1][y - 1] != 'x')
            {
                int counter = 0;
                while (x > 0 && y > 0 && test[x - 1][y - 1] != 'x' && test[x][y] != 'e')
                {
                    test[x][y] = 'x';
                    counter++;
                    x--;
                    y--;
                }
                steps++;
                if (CheckQueenMove(test, x, y, steps, resultsQueen) == false)
                {
                    steps--;
                    while (counter > 0)
                    {
                        x++;
                        y++;
                        test[x][y] = '#';
                        counter--;
                    }
                }
            }
            if (test[x][y] == 'e')
            {
                resultsQueen.Add(steps);
                return false;
            }
            return false;
        }

        /// задание 5) Жадина
        public static long CalculateMaxCoins(int[][] mapData, int idStart, int idFinish)
        {
            long maxMoney = 0;
            int collectedМoney = 0;
            int yourPosition = idStart;
            List<int> howMuchcanGet = new List<int>();
            List<int> blockCity = new List<int>();
            WayToTrevel(mapData, yourPosition, idFinish, howMuchcanGet, collectedМoney, blockCity);

            foreach (var m in howMuchcanGet)
            {
                if (m > maxMoney)
                {
                    maxMoney = m;
                }
                else if (m <= maxMoney)
                {
                    break;
                }
            }
            if (maxMoney == 0)
            {
                maxMoney = -1;
            }
            return maxMoney;
        }

        public static bool WayToTrevel(int[][] mapData, int yourPosition, int idFinish, List<int> money, int collectedМoney, List<int> blockCity)
        {
            for (int i = 0; i < mapData.Length; i++)
            {
                for (int j = 0; j < mapData[i].Length - 1; j++)
                {
                    if (!blockCity.Contains(mapData[i][j]) && j == 0 && mapData[i][j] == yourPosition && yourPosition != idFinish)
                    {
                        blockCity.Add(mapData[i][j]);
                        yourPosition = mapData[i][j + 1];
                        collectedМoney += mapData[i][j + 2];
                        if (!WayToTrevel(mapData, yourPosition, idFinish, money, collectedМoney, blockCity))
                        {
                            collectedМoney -= mapData[i][j + 2];
                            yourPosition = mapData[i][j];
                            blockCity.Remove(mapData[i][j]);
                        }
                    }
                    else if (!blockCity.Contains(mapData[i][j]) && j == 1 && mapData[i][j] == yourPosition && yourPosition != idFinish)
                    {
                        blockCity.Add(mapData[i][j]);
                        yourPosition = mapData[i][j - 1];
                        collectedМoney += mapData[i][j + 1];
                        if (!WayToTrevel(mapData, yourPosition, idFinish, money, collectedМoney, blockCity))
                        {
                            collectedМoney -= mapData[i][j + 1];
                            yourPosition = mapData[i][j];
                            blockCity.Remove(mapData[i][j]);
                        }
                    }
                    else if (yourPosition == idFinish)
                    {
                        money.Add(collectedМoney);
                        return false;
                    }
                }
            }
            return false;
        }

        /// Тесты (можно/нужно добавлять свои тесты) 

        private static void TestGenerateWordsFromWord()
        {
            var wordsList = new List<string>
            {
                "кот", "ток", "око", "мимо", "гром", "ром", "мама",
                "рог", "морг", "огр", "мор", "порог", "бра", "раб", "зубр"
            };

            AssertSequenceEqual(GenerateWordsFromWord("арбуз", wordsList), new[] { "бра", "зубр", "раб" });
            AssertSequenceEqual(GenerateWordsFromWord("лист", wordsList), new List<string>());
            AssertSequenceEqual(GenerateWordsFromWord("маг", wordsList), new List<string>());
            AssertSequenceEqual(GenerateWordsFromWord("погром", wordsList), new List<string> { "гром", "мор", "морг", "огр", "порог", "рог", "ром" });
        }

        private static void TestMaxLengthTwoChar()
        {
            AssertEqual(MaxLengthTwoChar("beabeeab"), 5);
            AssertEqual(MaxLengthTwoChar("а"), 0);
            AssertEqual(MaxLengthTwoChar("ab"), 2);
        }

        private static void TestGetPreviousMaxDigital()
        {
            AssertEqual(GetPreviousMaxDigital(21), 12l);
            AssertEqual(GetPreviousMaxDigital(531), 513l);
            AssertEqual(GetPreviousMaxDigital(1027), -1l);
            AssertEqual(GetPreviousMaxDigital(2071), 2017l);
            AssertEqual(GetPreviousMaxDigital(207034), 204730l);
            AssertEqual(GetPreviousMaxDigital(135), -1l);
        }

        private static void TestSearchQueenOrHorse()
        {
            char[][] gridA =
            {
                new[] {'s', '#', '#', '#', '#', '#'},
                new[] {'#', 'x', 'x', 'x', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', '#', 'e'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridA), new[] { 3, 2 });

            char[][] gridB =
            {
                new[] {'s', '#', '#', '#', '#', 'x'},
                new[] {'#', 'x', 'x', 'x', 'x', '#'},
                new[] {'#', 'x', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'x', '#', '#', '#', '#', 'e'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridB), new[] { -1, 3 });

            char[][] gridC =
            {
                new[] {'s', '#', '#', '#', '#', 'x'},
                new[] {'x', 'x', 'x', 'x', 'x', 'x'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', 'e', 'x', '#'},
                new[] {'x', '#', '#', '#', '#', '#'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridC), new[] { 2, -1 });


            char[][] gridD =
            {
                new[] {'e', '#'},
                new[] {'x', 's'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridD), new[] { -1, 1 });

            char[][] gridE =
            {
                new[] {'e', '#'},
                new[] {'x', 'x'},
                new[] {'#', 's'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridE), new[] { 1, -1 });

            char[][] gridF =
            {
                new[] {'x', '#', '#', 'x'},
                new[] {'#', 'x', 'x', '#'},
                new[] {'#', 'x', '#', 'x'},
                new[] {'e', 'x', 'x', 's'},
                new[] {'#', 'x', 'x', '#'},
                new[] {'x', '#', '#', 'x'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridF), new[] { -1, 5 });
        }

        private static void TestCalculateMaxCoins()
        {
            var mapA = new[]
            {
                new []{0, 1, 1},
                new []{0, 2, 4},
                new []{0, 3, 3},
                new []{1, 3, 10},
                new []{2, 3, 6},
            };

            AssertEqual(CalculateMaxCoins(mapA, 0, 3), 11l);

            var mapB = new[]
            {
                new []{0, 1, 1},
                new []{1, 2, 53},
                new []{2, 3, 5},
                new []{5, 4, 10}
            };

            AssertEqual(CalculateMaxCoins(mapB, 0, 5), -1l);

            var mapC = new[]
            {
                new []{0, 1, 1},
                new []{0, 3, 2},
                new []{0, 5, 10},
                new []{1, 2, 3},
                new []{2, 3, 2},
                new []{2, 4, 7},
                new []{3, 5, 3},
                new []{4, 5, 8}
            };

            AssertEqual(CalculateMaxCoins(mapC, 0, 5), 19l);
        }

        /// Тестирующая система, лучше не трогать этот код

        private static void Assert(bool value)
        {
            if (value)
            {
                return;
            }

            throw new Exception("Assertion failed");
        }

        private static void AssertEqual(object value, object expectedValue)
        {
            if (value.Equals(expectedValue))
            {
                return;
            }

            throw new Exception($"Assertion failed expected = {expectedValue} actual = {value}");
        }

        private static void AssertSequenceEqual<T>(IEnumerable<T> value, IEnumerable<T> expectedValue)
        {
            if (ReferenceEquals(value, expectedValue))
            {
                return;
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (expectedValue is null)
            {
                throw new ArgumentNullException(nameof(expectedValue));
            }

            var valueList = value.ToList();
            var expectedValueList = expectedValue.ToList();

            if (valueList.Count != expectedValueList.Count)
            {
                throw new Exception($"Assertion failed expected count = {expectedValueList.Count} actual count = {valueList.Count}");
            }

            for (var i = 0; i < valueList.Count; i++)
            {
                if (!valueList[i].Equals(expectedValueList[i]))
                {
                    throw new Exception($"Assertion failed expected value at {i} = {expectedValueList[i]} actual = {valueList[i]}");
                }
            }
        }

    }

}