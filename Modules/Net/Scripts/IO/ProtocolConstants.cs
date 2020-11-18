 namespace Pixeltron.Net.IO
 {
    [System.Serializable]
    public static class EntityActionType
    {
        public const string MOVE = "move";
        public const string CAST = "cast";
        public const string USE = "use";
        public const string EQUIP = "equip";
        public const string UNEQUIP = "unequip";
        public const string TAKE = "take";
        public const string DROP = "drop";
    }

    [System.Serializable]
    public static class EntityIOType
    {
        const int INVALID_NID = -1;
    }

    //events that we can get from the game server
    //must be kept in sync with Protocol.ts in the server
    [System.Serializable]
    public class GameServerEvent
    {
        public static string AUTHREQUEST = "authreq";
        public static string CONFIGURE = "configure";
        public static string CREATE = "create";
        public static string UPDATE = "update";
        public static string DESTROY = "destroy";
        public static string ACTIONSTATUS = "actionstatus";
        public static string MESSAGE = "message";
        public static string PONG1 = "pong1";
        public static string PONG2 = "pong2";
        
    }

    //events that we can send to the server
    //defined in server Protocol.ts
    [System.Serializable]
    public class GameClientEvent
    {
        public static string SOCKETCONNECT = "connection";
        public static string AUTHRESPONSE = "authr";
        public static string PLAYERINIT = "playerinit";
        public static string ACTION = "caction";
        public static string DISCONNECT = "cdisconnect";
        public static string MESSAGE = "message";
        public static string PING1 = "ping1";
        public static string PING2 = "ping2";
        
    }

    [System.Serializable]
    public class GameServerEventField
    {
        public const string CONFIGURE_PLAYERNID         = "playernid";
        public const string ACTION_STATUS_COMPLETED     = "completed";
        public const string ACTION_STATUS_INVALID       = "invalid";
        public const string ACTION_STATUS_CANCELLED     = "cancelled";
        public const string ACTION_STATUS_INCOMPLETE    = "incomplete";

    }
 }
