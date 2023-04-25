package models;

public class Car {

    private int id;
    private int size;
    private int posX;
    private int posY;
    private boolean vertical;

    public Car(int id, int size, int posX, int posY, boolean vertical) {
        this.id = id;
        this.size = size;
        this.posX = posX;
        this.posY = posY;
        this.vertical = vertical;
    }

    int getId() {
        return id;
    }

    int getSize() {
        return size;
    }

    int getPosX() {
        return posX - 1;
    }

    void setPosX(int posX) {
        this.posX = posX + 1;
    }

    int getPosY() {
        return posY - 1;
    }

    void setPosY(int posY) {
        this.posY = posY + 1;
    }

    boolean isVertical() {
        return vertical;
    }

    Car copy() {
        return new Car(id, size, posX, posY, vertical);
    }
}
