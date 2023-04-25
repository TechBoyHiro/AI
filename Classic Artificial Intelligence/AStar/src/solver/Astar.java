package solver;

import models.Node;

import java.util.PriorityQueue;
import java.util.Set;
import java.util.TreeSet;

public class Astar {

    public void aStar(Node root) {
        Set<Long> visited = new TreeSet<>();
        PriorityQueue<Node> fringe = new PriorityQueue<>();
        fringe.add(root);

        Node temp = null;
        boolean solved = false;

        while (!fringe.isEmpty()) {
            temp = fringe.poll();
            if (!visited.contains(temp.hash())) {
                visited.add(temp.hash());
                if (temp.isFinal()) {
                    solved = true;
                    break;
                }
                fringe.addAll(temp.successor());
            }
        }

        if (!solved) {
            System.out.println("never mind");
            return;
        }

        temp.printPath();
    }

}
