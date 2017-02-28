% Garrett Scholtes
% 2017-02-25
% IMPORTANT: README:
% You must download "csv.tar.gz" from the Discord server first and 
% unpack it.  The CSV's should be placed in the folder "csv/" in the same
% directory as this script (when unpacking csv.tar.gz, it may put them in
% additional nested directories... you essentially need to flatten them)

clear all;
close all;

% We are ignoring 31.csv since it is only a partial sample
DEPTH = 30;

% Which subset of attributes to use? 
bextract = 4:12;

% Seed 
rng(2017);

% Explanation of what is inside the matrix 'data':
%
% Every "cross section" of data (that is, each data(:,:,k)) is the data
% from an individual csv file.
%
% Each of these cross sections is of the same format as the 'data' matrix
% from 'linear_regression.m'.  Every cross section represents a different
% time position in all the songs (i.e., 1.csv is at the beginning of each
% song, 2.csv is from the next set of samples, ..., 31.csv is the end of 
% each song
csv_sizes = csvread('csv\1.csv');
data = zeros([size(csv_sizes) DEPTH]);
for k = 1:DEPTH
    data(:,:,k) = csvread(sprintf('csv\\%d.csv', k));
end

permutation = randperm(size(data, 1));
% Train on 2/3, leave 1/3 for testing
TRAIN_SIZE_RAW = floor(2/3 * size(data, 1));
train_set_raw = data(permutation(:,1:TRAIN_SIZE_RAW),:,:);
test_set = data(permutation(:,TRAIN_SIZE_RAW+1:end),:,:);

% We need to reshape the training set to only be rows of observations
TRAIN_SIZE = TRAIN_SIZE_RAW*DEPTH;
train_set = permute(train_set_raw, [1 3 2]);
train_set = reshape(train_set, [], size(train_set_raw,2), 1);

% bextract training
train_attr = train_set(:,bextract);
train_pos = train_set(:,2);
train_int = train_set(:,3);

% bextract testing
test_attr = test_set(:,bextract,:);
test_pos = test_set(:,2,1);
test_int = test_set(:,3,1);
std_pos = test_set(:,67,1);
std_int = test_set(:,66,1);

%% Linear regression (arbitrary plane - not necessarily through origin)
train_attr_mod = [ones(TRAIN_SIZE,1) train_attr];
test_attr_mod = [ones(size(test_attr,1),1,DEPTH) test_attr];

%% Perform regression

regress_pos = train_attr_mod\train_pos;
regress_int = train_attr_mod\train_int;

results_pos_raw = zeros(size(test_set,1),DEPTH);
results_int_raw = zeros(size(test_set,1),DEPTH);
for k = 1:DEPTH
    results_pos_raw(:,k) = test_attr_mod(:,:,k) * regress_pos;
    results_int_raw(:,k) = test_attr_mod(:,:,k) * regress_int;
end

results_pos = mean(results_pos_raw, 2);
results_int = mean(results_int_raw, 2);

%% Results

accuracy_pos = sum(abs(results_pos - test_pos) < std_pos)/size(test_set,1);
accuracy_int = sum(abs(results_int - test_int) < std_int)/size(test_set,1);
accuracy = accuracy_pos*accuracy_int;

RMSE_pos = sqrt(sum((results_pos - test_pos).^2/size(test_set,1)));
RMSE_int = sqrt(sum((results_int - test_int).^2/size(test_set,1)));

mean_pos = mean(test_pos);
mean_int = mean(test_int);
R2_pos = 1 - sum((results_pos-mean_pos).^2)/sum((test_pos-mean_pos).^2);
R2_int = 1 - sum((results_int-mean_int).^2)/sum((test_int-mean_int).^2);

































