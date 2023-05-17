#include <iostream>
#include <cmath>
#include <omp.h>

// Функція, яку інтегруємо
double f(double x) {
    return x * x; // Замініть цю функцію на власну
}

// Обчислення інтегралу методом прямокутників
double computeRectangleMethod(double a, double b, int numRectangles) {
    double dx = (b - a) / numRectangles;
    double sum = 0.0;

#pragma omp parallel for reduction(+:sum)
    for (int i = 0; i < numRectangles; i++) {
        double x = a + (i + 0.5) * dx;
        double height = f(x);
        sum += height * dx;
    }

    return sum;
}

// Обчислення інтегралу методом трапецій
double computeTrapezoidMethod(double a, double b, int numTrapezoids) {
    double dx = (b - a) / numTrapezoids;
    double sum = 0.0;

#pragma omp parallel for reduction(+:sum)
    for (int i = 0; i < numTrapezoids; i++) {
        double x1 = a + i * dx;
        double x2 = a + (i + 1) * dx;
        double height1 = f(x1);
        double height2 = f(x2);
        sum += (height1 + height2) * 0.5 * dx;
    }

    return sum;
}

// Обчислення інтегралу методом Сімпсона
double computeSimpsonMethod(double a, double b, int numSegments) {
    double dx = (b - a) / numSegments;
    double simpson_integral = 0.0;

#pragma omp parallel for reduction(+:simpson_integral)
    for (int step = 0; step < numSegments; step++) {
        double x1 = a + step * dx;
        double x2 = a + (step + 1) * dx;

        simpson_integral += (x2 - x1) / 6.0 * (f(x1) + 4.0 * f(0.5 * (x1 + x2)) + f(x2));
    }

    return simpson_integral;
}

int main() {
    double a = 0.0; // Початок інтервалу
    double b = 1.0; // Кінець інтервалу
    int numRectangles = 1000000; // Кількість прямокутників
    int numTrapezoids = 1000000; // Кількість трапецій
    int numSegments = 1000000; // Кількість сегментів

    double resultRectangles = computeRectangleMethod(a, b, numRectangles);
    double resultTrapezoids = computeTrapezoidMethod(a, b, numTrapezoids);
    double resultSimpson = computeSimpsonMethod(a, b, numSegments);

    std::cout << "Result using Rectangle Method: " << resultRectangles << std::endl;
    std::cout << "Result using Trapezoid Method: " << resultTrapezoids << std::endl;
    std::cout << "Result using Simpson Method: " << resultSimpson << std::endl;

    return 0;
}