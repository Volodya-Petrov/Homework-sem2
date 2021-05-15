
namespace CalculatorBasedOnStack
{   
    /// <summary>
    ///  интерфейс структуры данных типа "first in last out"
    /// </summary>
    public interface IStack
    {
        /// <summary>
        /// снимает элемент со стека
        /// </summary>
        double Pop();

        /// <summary>
        /// добавляет элемент в стек
        /// </summary>
        void Push(double number);

        /// <summary>
        /// проверяет пуст ли стек
        /// </summary>
        bool IsEmpty();
    }
}
