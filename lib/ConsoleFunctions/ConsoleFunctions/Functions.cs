using System;

namespace ConsoleFunctions
{
	public static class Functions
	{
	        public static object Print(object[] args)
        	{
	            Console.Write(arrayToString(args).Replace("\\n", "\n"));
        	    return null;
        	}

	        public static object Input(object[] args)
        	{
	            return Console.ReadLine();
       	 	}

	        public static object Cls(object[] args)
        	{
	            Console.Clear();
        	    return null;
        	}

	        public static object Pause(object[] args)
        	{
	            Console.ReadKey(true);
        	    return null;
	        }

	        public static object Setfcol(object[] args)
        	{
	            Console.ForegroundColor = parseColor(args[0].ToString());
        	    return null;
	        }

	        public static object Setbcol(object[] args)
        	{
	            Console.BackgroundColor = parseColor(args[0].ToString());
        	    return null;
	        }

	        public static object Getfcol(object[] args)
        	{
	            return Console.ForegroundColor.ToString();
        	}

	        public static object Getbcol(object[] args)
        	{
	            return Console.BackgroundColor.ToString();
        	}
 
	        private static object[] narrowArray(object[] args, int startIndex)
        	{
	            object[] result = new object[args.Length];

	            for (int x = startIndex; x < args.Length; x++)
        	        result[x] = args[x];

	            return result;
	        }

	        private static string arrayToString(object[] args, int startIndex = 0)
        	{
	            string result = "";

        	    for (int x = startIndex; x < args.Length; x++)
                	result += args[x].ToString();

	            return result;
        	}

	        private static ConsoleColor parseColor(string color)
        	{
	            switch(color)
        	    {
                	case "red":
                    		return ConsoleColor.Red;
                	case "yellow":
                		return ConsoleColor.Yellow;
                	case "green":
                    		return ConsoleColor.Green;
                	case "blue":
                    		return ConsoleColor.Blue;
                	case "white":
                    		return ConsoleColor.White;
                	case "black":
                    		return ConsoleColor.Black;
                	default:
                    		throw new Exception("Color is not valid!");
            	   }
        	}
	}
}
