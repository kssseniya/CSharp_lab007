using System;
using System.Reflection;
using System.Xml.Linq;
using AnimalLibrary;

namespace lab007
{
    class Program
    {
        static void Main()
        {
            //Создаем XML-документ
            var xmlDocument = new XDocument(new XElement("Library"));

            //Загружаем сборку библиотеки классов
            var assembly = Assembly.Load("AnimalLibrary");

            //Проходим по всем классам в библиотеке
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass && !type.IsAbstract) continue;

                //Создаем элемент для каждого класса
                var classElement = new XElement("Class",
                    new XAttribute("Name", type.Name),
                    new XAttribute("IsAbstract", type.IsAbstract));

                //Добавляем пользовательский атрибут Comment, если он существует
                var commentAttribute = type.GetCustomAttribute<CommentAttribute>();
                if (commentAttribute != null)
                {
                    classElement.Add(new XElement("Comment", commentAttribute.Comment));
                }

                //Добавляем информацию о свойствах класса
                var propertiesElement = new XElement("Properties");
                foreach (var prop in type.GetProperties())
                {
                    propertiesElement.Add(new XElement("Property",
                        new XAttribute("Name", prop.Name),
                        new XAttribute("Type", prop.PropertyType.Name)));
                }
                classElement.Add(propertiesElement);

                //Добавляем информацию о методах класса
                var methodsElement = new XElement("Methods");
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    //Пропускаем методы доступа get/set
                    if (method.IsSpecialName) continue;

                    var methodElement = new XElement("Method",
                        new XAttribute("Name", method.Name),
                        new XAttribute("ReturnType", method.ReturnType.Name));

                    //Добавляем параметры метода
                    var parametersElement = new XElement("Parameters");
                    foreach (var parameter in method.GetParameters())
                    {
                        parametersElement.Add(new XElement("Parameter",
                            new XAttribute("Name", parameter.Name),
                            new XAttribute("Type", parameter.ParameterType.Name)));
                    }
                    methodElement.Add(parametersElement);
                    methodsElement.Add(methodElement);
                }
                classElement.Add(methodsElement);

                //Добавляем класс к корневому элементу
                xmlDocument.Root.Add(classElement);
            }

            //Сохраняем XML-документ в файл
            string filePath = "ClassDiagram.xml";
            xmlDocument.Save(filePath);
            Console.WriteLine($"XML-файл успешно сгенерирован и сохранен как {filePath}");
        }
    }
}
