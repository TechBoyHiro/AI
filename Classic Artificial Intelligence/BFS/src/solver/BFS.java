package solver;

import models.Node;

import java.util.LinkedList;

public class BFS {

    public void bfs(Node root) {
        LinkedList<Node> fringe = new LinkedList<>();
        fringe.add(root);

        Node temp = null;
        boolean solved = false;

        while (!fringe.isEmpty()) {
            temp = fringe.pop();
            if (temp.isFinal()) {
                solved = true;
                break;
            }
            fringe.addAll(temp.successor());
        }

        if (!solved) {
            System.out.println("never mind");
            return;
        }

        temp.printPath();

    }


}
