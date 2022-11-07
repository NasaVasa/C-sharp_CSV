<h1> C-sharp_CSV </h1>
<h2> 5 November 2022 - 7 November 2022 </h2>
<h3> Программа разработана в качестве решения СР по предмету.</h3>
<h4>Подготовьте программу обработки данных, получаемых из набора данных о зарплатах в
секторе наук о данных. Первичные данные для работы программы находятся в файле
Data_Science_Fields_Salary_Categorization.csv
(https://www.kaggle.com/datasets/whenamancodes/data-science-fields-salary-categorization).
Программа должна предоставлять пользователю текстовое меню, позволяющее
обратиться к следующим операциям:</h4>
<p>1. Вводить адрес файла, из которого загружаются данные о работниках сферы наук
о данных. Обратите внимание, что название файла может быть произвольным,
структура файла должна совпадать со структурой первичного файла</p>
<p>2. Выводить на экран из набора исходных данных информацию о группах
работников по Experience.</p>
<p>a. Сохранять в файл workers.csv группируя по опыту работы.</p>
<p>3. Выводить на экран список работников, группируя их по году работы, для каждой
группы перед ней указать самую большую зарплату в группе.</p>
<p>a. Сохранять перечень работников по годам в файле Workers-N.csv, где N -
номер года.</p>
<p>4. Выводить на экран выборку работников, зарплата которых находится в
диапазоне от 70 до 80% от максимальной.</p>
<p>a. Сохраните выборку в файл Salary-workers.csv</p>
<p>5. Выводить на экран сводную статистику по данным загруженного файла:</p>
<p>a. Общее количество строк с данными</p>
<p>b. Работников с наибольшей и наименьшей зарплатой.</p>
<p>c. Количество Data Engineer работающих из GB.</p>
<p>d. Количество работников работающих в компаниях из GB, но работающих из
иной страны, перед каждым из них написать из какой страны он работает.</p>
<h4>Требования к работе со входными и выходными данными:</h4>
<p>1. Программа должна сохранять работоспособность при вводе некорректных
адресов и имён файлов, с учётом различных платформ запуска файлов.</p>
<p>2. Некорректно структурированный файл программой не обрабатывается,
пользователю выводится сообщение об ошибке и предлагается вернуться к
основному меню.</p>
<p>3. Программа обязательно должна корректно открывать созданные ей файлы и
позволять выполнять над ними все операции из меню.</p>
<p>4. Программа автоматически разбирается с кодировками файла и отображает
данные на экран и в файлы человекочитаемом виде.</p>