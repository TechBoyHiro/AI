package models;

import java.util.ArrayList;

public class Node implements Comparable<Node> {

    private ArrayList<Car> cars;
    private Node parent;
    private int[][] map;

    private int heuristic;//h()
    private int cost;//g()
    private int totalCost;//f() = h() + g()

    public Node(ArrayList<Car> cars) {
        map = new int[6][6];
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++) {
                map[i][j] = 0;
            }
        }
        for (Car temp : cars) {
            for (int i = 0; i < temp.getSize(); i++) {
                if (temp.isVertical())
                    map[temp.getPosX() + i][temp.getPosY()] = temp.getId();
                else
                    map[temp.getPosX()][temp.getPosY() + i] = temp.getId();
            }
        }
        this.cars = cars;
        this.parent = null;
        fillHeuristic();
    }

    private Node(ArrayList<Car> cars, Node parent, int[][] map) {
        this.parent = parent;
        this.map = map;
        this.cars = cars;
        fillHeuristic();
    }

    public void fillHeuristic() {
        if (parent == null) {
            this.cost = 0;
        } else {
            this.cost = parent.cost + 1;
        }
        this.heuristic = heuristic();
        this.totalCost = this.heuristic + this.cost;
    }

    private int heuristic() {
        return 0;
    }

    private void printMap() {
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++) {
                System.out.print(map[i][j] + " ");
            }
            System.out.println(" ");
        }
        System.out.println(" ");
    }

    private void removeCarFromMap(int[][] map, Car car) {
        for (int h = 0; h < car.getSize(); h++) {
            if (car.isVertical())
                map[car.getPosX() + h][car.getPosY()] = 0;
            else
                map[car.getPosX()][car.getPosY() + h] = 0;
        }
    }

    private void addCarFromMap(int[][] map, Car car) {
        for (int h = 0; h < car.getSize(); h++) {
            if (car.isVertical())
                map[car.getPosX() + h][car.getPosY()] = car.getId();
            else
                map[car.getPosX()][car.getPosY() + h] = car.getId();
        }
    }

    public ArrayList<Node> successor() {
        ArrayList<Node> children = new ArrayList<>();
        for (Car car : cars) {
            if (car.isVertical()) {
                for (int i = car.getPosX() - 1; i > -1; i--) {
                    if (moveCar(children, car, i)) {
                        break;
                    }
                }
                for (int i = car.getPosX() + car.getSize(); i < 6; i++) {
                    if (moveCar(children, car, i)) {
                        break;
                    }
                }
            } else {
                for (int j = car.getPosY() - 1; j > -1; j--) {
                    if (moveCarHor(children, car, j)) {
                        break;
                    }
                }
                for (int j = car.getPosY() + car.getSize(); j < 6; j++) {
                    if (moveCarHor(children, car, j)) {
                        break;
                    }
                }

            }
        }
        return children;
    }

    private boolean moveCarHor(ArrayList<Node> children, Car car, int j) {
        if (map[car.getPosX()][j] == 0) {
            Node child = this.copy();
            removeCarFromMap(child.map, car);
            for (Car c : child.cars) {
                if (c.getId() == car.getId()) {
                    if (j > c.getPosY()) {
                        c.setPosY(j - c.getSize() + 1);
                    } else {
                        c.setPosY(j);
                    }
                    addCarFromMap(child.map, c);
                }
            }
            child.parent = this;
            child.fillHeuristic();
            children.add(child);

            return false;
        }
        return true;
    }

    private boolean moveCar(ArrayList<Node> children, Car car, int i) {
        if (map[i][car.getPosY()] == 0) {
            Node child = this.copy();
            removeCarFromMap(child.map, car);
            for (Car c : child.cars) {
                if (c.getId() == car.getId()) {
                    if (i > c.getPosX()) {
                        c.setPosX(i - c.getSize() + 1);
                    } else {
                        c.setPosX(i);
                    }
                    addCarFromMap(child.map, c);
                }
            }
            child.parent = this;
            child.fillHeuristic();
            children.add(child);
            return false;
        }
        return true;
    }

    public boolean isFinal() {
        return map[2][4] == map[2][5] && map[2][5] == 1;
    }

    private Node copy() {
        int[][] map = new int[6][6];
        for (int i = 0; i < 6; i++) {
            System.arraycopy(this.map[i], 0, map[i], 0, 6);
        }
        ArrayList<Car> carArrayList = new ArrayList<>();
        for (Car car : cars) {
            carArrayList.add(car.copy());
        }
        return new Node(carArrayList, parent, map);
    }

    public long hash() {
        long hash = 1;
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++) {
                long hashBase = 67;
                hash *= hashBase;
                hash += (this.map[i][j]);
                long hashMode = (long) (1e9 + 7);
                hash %= hashMode;
            }
        }
        return hash;
    }

    private void print_single_move(Node first , Node second){
        for (Car fcar : first.cars){
            for (Car scar : second.cars){
                if(fcar.getId() != scar.getId())
                    continue;
                if(fcar.getPosX() > scar.getPosX()){
                    System.out.println(fcar.getId() + " u " + (fcar.getPosX() - scar.getPosX()));
                    return;
                }
                if(fcar.getPosX() < scar.getPosX()){
                    System.out.println(fcar.getId() + " d " + (scar.getPosX() - fcar.getPosX()));
                    return;
                }
                if(fcar.getPosY() > scar.getPosY()){
                    System.out.println(fcar.getId() + " l " + (fcar.getPosY() - scar.getPosY()));
                    return;
                }
                if(fcar.getPosY() < scar.getPosY()){
                    System.out.println(fcar.getId() + " r " + (scar.getPosY() - fcar.getPosY()));
                    return;
                }
            }
        }


    }

    public void printPath() {
        Node temp = this;
        ArrayList<Node> nodes = new ArrayList<>();
        while (temp.parent != null) {
            nodes.add(temp);
            temp = temp.parent;
        }
        nodes.add(temp);
        for (int i = nodes.size()-1 ; i > 0; i--) {
            // nodes.get(i).printMap();
            print_single_move(nodes.get(i), nodes.get(i-1));
        }
    }

    @Override
    public int compareTo(Node node) {
        return this.totalCost - node.totalCost;
    }
}
