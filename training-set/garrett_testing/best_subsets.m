% Garrett Scholtes
% 2017-02-03

data = xlsread('..\ratings\song_database\annotations_with_features.xlsx');

% Seed 
rng(2017);

permutation = randperm(size(data, 1));
% Train on 2/3, leave 1/3 for testing 
TRAIN_SIZE = floor(2/3 * size(data, 1));
train_set = data(permutation(:,1:TRAIN_SIZE),:);
test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

best_subset_int = [];
best_subset_pos = [];
best_acc_int = 0;
best_acc_pos = 0;

% Number of subsets to try for each size
SUBTRIALS = 10000;

for count = 1:31
    %fprintf('Round %d...\n', count);
    for k = 1:SUBTRIALS
        % Which bextract subset
        bextract = randsample(4:34, count);
        
        % bextract training
        train_attr = train_set(:,bextract);
        train_pos = train_set(:,2);
        train_int = train_set(:,3);

        % bextract testing
        test_attr = test_set(:,bextract);
        test_pos = test_set(:,2);
        test_int = test_set(:,3);
        std_pos = test_set(:,67);
        std_int = test_set(:,66);

        %%% Un-comment one of the sections below
        %% Linear regression (arbitrary plane - not necessarily through origin)
        train_attr_mod = [ones(TRAIN_SIZE,1) train_attr];
        test_attr_mod = [ones(size(test_attr,1),1) test_attr];

        %% Quadratic regression
        %train_attr_mod = [ones(TRAIN_SIZE,1) train_attr train_attr.^2];
        %test_attr_mod = [ones(size(test_attr,1),1) test_attr test_attr.^2];


        %% Perform regression

        regress_pos = train_attr_mod\train_pos;
        regress_int = train_attr_mod\train_int;

        results_pos = test_attr_mod * regress_pos;
        results_int = test_attr_mod * regress_int;

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
        
        %% Update maxima
        if accuracy_pos > best_acc_pos
            best_acc_pos = accuracy_pos;
            best_subset_pos = bextract;
            fprintf('pos! %6.6d    %0.4f   %0.4f\n', SUBTRIALS*count+k, best_acc_pos, best_acc_int);
        end
        if accuracy_int > best_acc_int
            best_acc_int = accuracy_int;
            best_subset_int = bextract;
            fprintf('int! %6.6d    %0.4f   %0.4f\n', SUBTRIALS*count+k, best_acc_pos, best_acc_int);
        end
    end
end