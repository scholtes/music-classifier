function [ N ] = normalize( A, k, min1, Max1 )
% Normalize data in A (per column), excluding k outliers from each end 
% and fitting new [non-outlier data] into range [min1, Min1]).
% A different min/max range to normalize to can be chosen for each
% attribute, so min1 and max1 are `1`x`size(A,2)` vectors
A_sort = sort(A);
min0 = A_sort(k,:);
Max0 = A_sort(end-k,:);

coeff = repmat((Max1-min1)./(Max0-min0), size(A,1), 1);
min0rep = repmat(min0, size(A,1), 1);
min1rep = repmat(min1, size(A,1), 1);

N = coeff.*(A - min0rep) + min1rep;

end

