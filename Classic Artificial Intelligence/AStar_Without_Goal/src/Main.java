import models.Car;
import models.Node;
import solver.Astar;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        Scanner scanner = new Scanner(System.in);
        int carCount = Integer.parseInt(scanner.nextLine());
        ArrayList<Car> cars = new ArrayList<>();
        for (int i = 0; i < carCount; i++) {
            String[] tempCar = scanner.nextLine().split(" ");
            boolean vertical = tempCar[3].equals("v");
            cars.add(new Car(Integer.parseInt(tempCar[0]), Integer.parseInt(tempCar[4]),
                    Integer.parseInt(tempCar[1]), Integer.parseInt(tempCar[2]), vertical));
        }

        Node root = new Node(cars);
        Astar astar = new Astar();
        astar.aStar(root);


    }


    /*
5
1 3 1 h 2
2 4 2 v 2
3 1 3 v 3
4 1 4 v 3
5 3 6 v 3

1
1 3 1 h 2

     */

}
