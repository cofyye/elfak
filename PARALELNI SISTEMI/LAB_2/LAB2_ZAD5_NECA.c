#include<stdio.h>
#include<mpi.h>
#define N 6

int main(int argc, char** argv) {
	int rank, P; // P = 3
	float a[N], b, c , sum_avg = 0 , avg, local_a[N] , local_sum = 0 , sum , izraz;
	int i;
	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	if (N % P != 0)
		return -1;
	if (rank == 0) {
		for (i = 0; i < N; i++)
		{
			a[i] = i+1;
			sum_avg += a[i];
		}
		avg = (float)sum_avg / N;
	}
	MPI_Bcast(&avg, 1, MPI_FLOAT, 0, MPI_COMM_WORLD);
	MPI_Scatter(a, N / P, MPI_FLOAT, local_a, N / P, MPI_FLOAT, 0 , MPI_COMM_WORLD);
	for (i = 0; i < N / P; i++) {
		local_sum += avg + local_a[i];
	}
	MPI_Reduce(&local_sum, &sum, 1, MPI_FLOAT, MPI_SUM, P-1 , MPI_COMM_WORLD);
	if (rank == P - 1) {
		b = 4.0;
		c = 6.0;
		izraz = 1 / (b + c) * sum;
		printf("izraz = %.2f\n", izraz);
	}


	MPI_Finalize();
	return 0;
}