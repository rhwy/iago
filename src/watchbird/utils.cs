namespace Utilities
{
	using System;
	using System.Collections.Generic;
	
	public static class ApplicationTime
	{
		public static Func<DateTime> NowDefinition {private get;set;} 
			= ()=>DateTime.Now;
		public static DateTime Now { get { return NowDefinition();}}
		
		static DateTime? start = null;
		public static DateTime StartTime 
		{
			get{return start.HasValue ? start.Value: Start() ;}
		}
		public static DateTime Start()
		{
			if(start == null)
			{
				start = Now;
			}
			return start.Value;
		}
		
		public static Func<string,string> GetStampDefinition = 
			(pattern) => 
				new DateTime((ApplicationTime.Now-StartTime).Ticks)
				.ToString(pattern ?? "ss:ffffff");
		
		public static string GetStamp(string pattern = null)
		{
			return GetStampDefinition(pattern);
		}
	}
	
	public class MatchNotFoundException : Exception 
	{ 
	    public MatchNotFoundException(string message) : base(message) { } 
	}
	
	public class PatternMatch<T, TResult> 
	{ 
	    private readonly T value; 
	    private readonly List<Tuple<Predicate<T>, Func<T, TResult>>> cases  
	        = new List<Tuple<Predicate<T>, Func<T, TResult>>>(); 
	    private Func<T, TResult> elseFunc;
	
	    internal PatternMatch(T value) 
	    { 
	        this.value = value; 
	    }
	
	    public PatternMatch<T, TResult> With(Predicate<T> condition, Func<T, TResult> result) 
	    { 
	        cases.Add(Tuple.Create(condition, result)); 
	        return this; 
	    }
	
	    public PatternMatch<T, TResult> Else(Func<T, TResult> result) 
	    { 
	        if (elseFunc != null) 
	            throw new InvalidOperationException("Cannot have multiple else cases");
	
	        elseFunc = result; 
	        return this; 
	    }
	
	    public TResult Do() 
	    { 
	        if (elseFunc != null) 
	            cases.Add( 
	                Tuple.Create<Predicate<T>, Func<T, TResult>>(x => true, elseFunc)); 
	        foreach (var item in cases) 
	            if (item.Item1(value)) 
	                return item.Item2(value);
	
	        throw new MatchNotFoundException("Incomplete pattern match"); 
	    } 
	}
	
	
	public class PatternMatchContext<T> 
	{ 
	    private readonly T value;
	    internal PatternMatchContext(T value) 
	    { 
	        this.value = value; 
	    }
	
	    public PatternMatch<T, TResult> With<TResult>( 
	        Predicate<T> condition,  
	        Func<T, TResult> result) 
	    { 
	        var match = new PatternMatch<T, TResult>(value); 
	        return match.With(condition, result); 
	    } 
	}
	
	public static class PatternMatchExtensions 
	{ 
	    public static PatternMatchContext<T> Match<T>(this T value) 
	    { 
	        return new PatternMatchContext<T>(value); 
	    } 
	}
}