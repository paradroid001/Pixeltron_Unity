namespace Pixeltron.Net.IO
{
   
    [System.Serializable]
    public class ActionStatus
    {
        public int sourcenid;
        public int aid;
        public string result;
        public float progress;
        public string msg;
    }

    [System.Serializable]
    public class EntityAction
    {
        private static int _nextid = 0;
        public int id;
        public string actiontype;
        //not really any other way to do this
        //which means again, there might need to 
        //be a type mapping / marshalling layer
        //when declaring actions.
        public string[] args;

        public EntityAction(string atype)
        {
            this.id = ++EntityAction._nextid;
            this.actiontype = atype;
            //TODO hardcoded to 5 args...
            this.args = new string[5];
            //this.args[0] = "arg1";
            //this.args[1] = "arg2";
        }

        public JSONObject json()
        {
            JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
            j.AddField("aid", this.id);
            j.AddField("action", this.actiontype);
            JSONObject args = new JSONObject(JSONObject.Type.ARRAY);
            foreach (var item in this.args)
            {
                args.Add(item);
            }
            j.AddField("args", args);
            return j;
        }
    }
}