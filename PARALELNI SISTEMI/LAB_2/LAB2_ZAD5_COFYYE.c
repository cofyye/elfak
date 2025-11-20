#include <stdio.h>
#include <mpi.h>

constexpr auto N = 6;

int main(int argc, char** argv) {
	int rank = -1, P = -1, root = 0, avg = 0, local_sum = 0, global_sum = 0, local_niz[N] = {}, niz[N] = {};
	
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);

	if (N % P != 0) {
		return -1;
	}

	if (rank == root) {
		niz[0] = 1;
		niz[1] = 5;
		niz[2] = 8;
		niz[3] = 9;
		niz[4] = 2;
		niz[5] = 6;

		for (int i = 0; i < N; i++) {
			avg += niz[i];
		}
		avg /= N;
	}

	MPI_Bcast(&avg, 1, MPI_INT, root, MPI_COMM_WORLD);
	MPI_Scatter(niz, N / P, MPI_INT, local_niz, N / P, MPI_INT, root, MPI_COMM_WORLD);

	for (int i = 0; i < N / P; i++) {
		local_sum += avg + local_niz[i];
	}

	printf("process %d local_sum %d\n", rank, local_sum);
	
	MPI_Reduce(&local_sum, &global_sum, 1, MPI_INT, MPI_SUM, P - 1, MPI_COMM_WORLD);

	if (rank == P - 1) {
		int b = 2;
		int c = 8;
		float s = (1.0 / (b + c)) * global_sum;
		printf("process %d je izracunao sumu %.2f\n", rank, s);
	}

	MPI_Finalize();

	return 0;
}