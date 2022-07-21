import java.io.Console;

public class main {

    public static void main(String[] args)
    {
        System.out.println("Part II starting");
        Thread thread = new NewThread();
        thread.start();

    }

    public static  class NewThread extends Thread
    {
        @Override
        public void run() {
            System.out.println("running thread");
        }
    }
}
