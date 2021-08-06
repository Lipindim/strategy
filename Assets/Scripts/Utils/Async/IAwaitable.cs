using System.Runtime.CompilerServices;

public interface IAwaitable<T>
{
	IAwaiter<T> GetAwaiter();
}