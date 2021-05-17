public enum MazeBlockType 
{
    OUTSIDE = ' ',

    ROAD = '-',

    UNDER_ITEM = '@',

    /**
     * Только в одну сторону (платформа).
     */
    DIOD = '^',

    DIOD_SWITCH = '!',

    DOOR_YELLOW = '1',
    DOOR_BUTTON_YELLOW = 'Y',

    DOOR_RED = '2',
    DOOR_BUTTON_RED = 'R',

    DOOR_GREEN = '3',
    DOOR_BUTTON_GREEN = 'G'
}
