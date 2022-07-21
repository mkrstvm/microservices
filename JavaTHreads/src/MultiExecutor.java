import java.util.AbstractList;
import java.util.ArrayList;
import java.util.List;

public class MultiExecutor
{
    private List<Runnable> Tasks;

    public MultiExecutor(List<Runnable> tasks)
    {
        this.Tasks = tasks;
    }

    public void ExecuteAll()
    {
        List<Thread> threads = new ArrayList<>(Tasks.size());
        for (Runnable task: Tasks)
        {
            Thread thread = new Thread(task);
            threads.add(thread);
        }

        for (Thread thread : threads)
        {
            thread.start();
        }
    }
}
