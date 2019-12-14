namespace Contracts.Parser
{
    public enum WorkType
    {
        //делай тут что посчитаешь нужным
        ParsingEnvironment, //Ну эт я тип имел в виду парсинг списка групп, времени, дней и т.д.
        ParsingTimetable, //Ну тут очевидно парсинг самого расписания
        EndUpWork //Ну вдруг нужна какая-то долгая завершающая обработка
    }
}