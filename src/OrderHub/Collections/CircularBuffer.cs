using System.Collections;
using System.Collections.Generic;

namespace OrderHub.Collections;

public class CircularBuffer<T> : IEnumerable<T>
{
    // ЗАДАЧА 1: объявите внутренний массив заданной ёмкости
    //   private readonly T[] buffer;
    private readonly T[] buffer;

    // ЗАДАЧА 2: объявите счётчики:
    //   private int next = 0;    // куда писать следующий элемент
    //   private int size = 0;    // сколько элементов сейчас хранится
    //   private readonly int capacity;
    private int next = 0;
    private int size = 0;
    private readonly int capacity;

    // ЗАДАЧА 3: конструктор принимает capacity и создаёт массив нужного размера
    // подсказка: если capacity <= 0, бросьте ArgumentOutOfRangeException
    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        this.capacity = capacity;
        buffer = new T[capacity];
    }

    public int Count => size;
    public bool IsFull => size == capacity;

    public void Add(T item)
    {
        // ЗАДАЧА 1: запишите элемент в buffer[next]
        // ЗАДАЧА 2: сдвиньте next по кольцу:
        //           next = (next + 1) % capacity;
        // ЗАДАЧА 3: если size < capacity — увеличьте size
        // ЗАДАЧА 4: если буфер заполнен — не трогайте size: старый элемент просто перезаписан
        // подсказка: перезапись старого элемента происходит автоматически,
        // нужно лишь не увеличивать size сверх capacity
        buffer[next] = item;
        next = (next + 1) % capacity;
        if (size < capacity)
            size++;
    }

    // ЗАДАЧА: реализуйте обход от самого старого хранящегося элемента к самому новому
    // подсказка: индекс самого старого — это (next - size + capacity) % capacity,
    // затем двигайтесь вперёд по кольцу
    public IEnumerator<T> GetEnumerator()
    {
        int index = (next - size + capacity) % capacity;
        for (int i = 0; i < size; i++)
        {
            yield return buffer[index];
            index = (index + 1) % capacity;
        }
    }

    // ЗАДАЧА: обратный обход — от самого нового к самому старому
    public IEnumerable<T> Reverse()
    {
        // подсказка: идите назад по кольцу: index = (index - 1 + capacity) % capacity
        int index = (next - 1 + capacity) % capacity;
        for (int i = 0; i < size; i++)
        {
            yield return buffer[index];
            index = (index - 1 + capacity) % capacity;
        }
    }

    // ЗАДАЧА: явная реализация неуниверсального метода — без неё foreach работать не будет
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
