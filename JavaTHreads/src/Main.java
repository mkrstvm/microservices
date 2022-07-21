public class Main {

    public static void main(String[] args)
    {
        Thread thread = new Thread(new Runnable() {
            @Override
            public void run() {
                System.out.println(Thread.currentThread().getName());
                throw new RuntimeException("exception");
            }
        });

        thread.setUncaughtExceptionHandler(new Thread.UncaughtExceptionHandler() {
            @Override
            public void uncaughtException(Thread t, Throwable e) {
                System.out.println(e.getMessage());
            }
        });
        thread.start();

        System.out.println(Thread.currentThread().getName());
    }
}
